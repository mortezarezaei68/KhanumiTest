
using System;
using Framework.Common;
using Framework.Controller.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketManagement.CommandHandler;
using TicketManagement.Configurations;
using TicketManagement.Query.Models;
using TicketManagement.Service.Config;

namespace TicketManagement.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _cfg = Configuration.Get<AppConfig>();
        }
        readonly AppConfig _cfg;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomController<AddTicketCommandRequestValidator>();
            services.TicketManagementDbInjections(Configuration);
            services.BootstrapEventTicketManagement();
            services.BootstrapTicketManagement();
            services.AddMassTransit(x =>
            {
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
                {
                    var url = _cfg.MassTransit.Host + (_cfg.MassTransit.Host.EndsWith("/") ? "" : "/") ;
                    c.Host(url, a =>
                    {
                        a.Username("guest");
                        a.Password("guest");
                    });
                    c.ConfigureEndpoints(context);
                }));
                
                x.AddRequestClient<GetAllUserRequest>();
            });

            services.AddMassTransitHostedService();
            services.AddCustomSwagger();
            services.AddCors(option =>
            {
                option.AddPolicy("EnableCorsForHttpOnly", builder =>
                {
                    builder.WithOrigins(
                            "https://localhost:5001",
                            "http://halfmilelogistics.com",
                            "https://halfmilelogistics.com",
                            "http://halfmilelogistics.com:3000",
                            "https://halfmilelogistics.com:3000",
                            "https://halfmilelogistics.com:8080",
                            "http://halfmilelogistics.com:8080",
                            "http://halfmilelogistics.com:9999",
                            "https://halfmilelogistics.com:9999",
                            "http://new.halfmilelogistics.com:9999",
                            "https://new.halfmilelogistics.com:9999",
                            "http://new.halfmilelogistics.com:3000",
                            "http://localhost:3000",
                            "http://localhost:9999",
                            "http://localhost",
                            "http://localhost:8080")
                                      
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.InitializeTicketManagementDatabase().Wait();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomSwagger();
            app.UseCors("EnableCorsForHttpOnly");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}