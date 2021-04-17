using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Common.Exceptions;
using Framework.Buses;
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Exception.Exceptions.Enum;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace TicketManagement.Configurations
{
     public static class CustomAuthenticationExtension
    {
        public static IServiceCollection AddCustomAuthenticationAuthorization(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    // options.Authority = configuration["JwtToken:Issuer"];
                    // options.Audience = configuration["JwtToken:Audience"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JwtToken:Issuer"],
                        ValidateIssuer = true,
                        ValidAudience = configuration["JwtToken:Audience"],
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameter) =>
                            expires >= DateTime.UtcNow,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x8{'5,C4ram)5zLq")),
                        // TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x8{'5,C4ram)5zLq"))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context =>
                        {
                            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                            {
                                context.Token =context.Request.Cookies["X-Access-Token"];
                            }
                            else if (context.Request.Cookies.ContainsKey("X-Refresh-Token"))
                            {
                                using var httpClientHandler = new HttpClientHandler();
                                httpClientHandler.ServerCertificateCustomValidationCallback =
                                    (message, cert, chain, errors) => true;
                                using var client = new HttpClient(httpClientHandler);
                                var refreshRequest = new RefreshAdminTokenRequest
                                {
                                    RefreshToken = context.Request.Cookies["X-Refresh-Token"]
                                };
                                                                try
                                {
                                    var res =
                                        await client.Post<RefreshAdminTokenRequest, RefreshAdminTokenResponse>(
                                            $"{configuration["AuthInterface-Url"]}/v1/AdminAuthentication/AdminUser/refreshToken",
                                            refreshRequest);
                                    if (res.IsSuccess)
                                    {
                                        context.Token = res.Data.AccessToken;
                                        var logger = context.HttpContext.RequestServices
                                            .GetRequiredService<ILoggerFactory>().CreateLogger(nameof(context));
                                        logger.LogError($"cookie url:{configuration["Cookie-Url"]}");
                                        var httpAccessor = context.HttpContext.RequestServices
                                            .GetRequiredService<IHttpContextAccessor>();
                                        httpAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token",
                                            res.Data.RefreshToken,
                                            new CookieOptions
                                            {
                                                Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                                                SameSite = SameSiteMode.Lax,
                                                Expires = DateTimeOffset.Now.AddDays(
                                                    int.Parse(configuration["JwtToken:ExpirationDays"])),
                                                Secure = false
                                            });
                                        httpAccessor.HttpContext.Response.Cookies.Append("X-Access-Token",
                                            res.Data.AccessToken,
                                        new CookieOptions
                                        {
                                            Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                                            SameSite = SameSiteMode.Lax,
                                            Expires = DateTimeOffset.Now.AddSeconds(int.Parse(configuration["JwtToken:AccessTokenExpiredTime"])),Secure = false
                                        });
                                    }
                                }
                                catch (AppException e)
                                {
                                    context.Response.Cookies.Delete("X-Refresh-Token");
                                }
                                // if (!data.IsSuccess)
                                // {
                                //     throw new AppException(ResultCode.BadRequest, data.Message,
                                //         HttpStatusCode.BadRequest);
                                // }
                                // var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(context));
                                // logger.LogError($"cookie url:{configuration["JwtToken:DomainUrl"]}");
                                // context.Token = data.Data.AccessToken;
                                // var httpAccessor=context.HttpContext.RequestServices.GetRequiredService<IHttpContextAccessor>();
                                // httpAccessor.HttpContext.Response.Cookies.Append("X-Refresh-Token", data.Data.RefreshToken,
                                //     new CookieOptions
                                //     {
                                //         Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                                //         SameSite = SameSiteMode.Lax, Expires = DateTimeOffset.Now.AddDays(int.Parse(configuration["JwtToken:ExpirationDays"])),Secure = false
                                //     });
                                // httpAccessor.HttpContext.Response.Cookies.Append("X-Access-Token", data.Data.AccessToken,
                                //     new CookieOptions
                                //     {
                                //         Domain = configuration["JwtToken:DomainUrl"], HttpOnly = true,
                                //         SameSite = SameSiteMode.Lax,
                                //         Expires = DateTimeOffset.Now.AddSeconds(int.Parse(configuration["JwtToken:AccessTokenExpiredTime"])),Secure = false
                                //     });
                            }
                        }
                    };
                });
            services.ConfigureApplicationCookie(options => {
                options.Cookie.Domain = configuration["JwtToken:DomainUrl"];
                options.Cookie.Path = "/";
            });
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("SuperClient", builder =>
            //     {
            //         builder.RequireClaim("scope","api1");
            //     });
            //     // options.AddPolicy("superuser", builder =>
            //     // {
            //     //     builder.RequireRole("superuser");
            //     // });
            // });
            services.AddAuthorization();
            return services;
        }
    }

     public class RefreshAdminTokenRequest
     {
         public string RefreshToken { get; set; }
     }
     public class RefreshAdminTokenResponse:ResponseCommand<RefreshTokenModel>
     {
         public RefreshAdminTokenResponse(bool isSuccess, ResultCode resultCode, RefreshTokenModel data, string message = null) : base(isSuccess, resultCode, data, message)
         {
         }
     }
     public class RefreshTokenModel
     {
         public string AccessToken { get; set; }
         public string RefreshToken { get; set; }
     }
}