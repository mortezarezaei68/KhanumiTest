using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common;
using Framework.Controller.Extensions;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contract.CustomerCommandHandlers;
using Service.Query.AdminUserQuery;
using UserManagement.Configurations;
using UserManagement.Service.Config;

namespace UserManagement.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            cfg = Configuration.Get<AppConfig>();
        }
        readonly AppConfig cfg;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.IdentityDbInjections(Configuration);
            services.BootstrapEventBusServices();
            services.BootstrapCustomizeServices();
            services.AddCustomController<AddCustomerCommandRequestValidator>();
            services.AddCustomAuthenticationAuthorization(Configuration);
            services.AddCustomSwagger();
            // services.AddMassTransit(x =>
            // {
            //     x.AddConsumer<GetAllUserConsumer>();
            //
            //     x.UsingRabbitMq((context, cfg) =>
            //     {
            //         cfg.ReceiveEndpoint(e =>
            //         {
            //             e.PrefetchCount = 16;
            //             e.UseMessageRetry(r => r.Interval(2, 3000));
            //             e.ConfigureConsumer<GetAllUserConsumer>(context);
            //         });
            //     } );
            // });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetAllUserConsumer>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(c =>
                {
                    c.Host(cfg.MassTransit.Host, a =>
                    {
                        a.Username("guest");
                        a.Password("guest");
                    });
                     // var url = cfg.MassTransit.Host + (cfg.MassTransit.Host.EndsWith("/") ? "" : "/") + cfg.MassTransit.Queue;
                    c.ReceiveEndpoint(cfg.MassTransit.Queue, e =>
                    {
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(2, 3000));
                        e.ConfigureConsumer<GetAllUserConsumer>(context);
                    });
                }));
            });

            services.AddMassTransitHostedService();
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
            app.UseCustomSwagger();
            app.InitializeUserDatabase().Wait();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
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