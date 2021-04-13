using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;

namespace UserManagement.Service
{
   public class Program
    {
        public static void Main(string[] args)
        {
            //configure logging first
            ConfigureLogging();

            //then create the host, so that if the host fails we can log errors
            CreateHost(args);
        }
        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;
            var isStaging = environment == Environments.Staging;
            
            if (isDevelopment)
            {    
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())    
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                    .Build();
                
                Log.Information("development");
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/myapp.txt")
                    .Enrich.WithProperty("Environment", environment)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
            else if (isStaging)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())    
                    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                    .Build();
                
                Log.Information("elastic search added");
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/myapp.txt")
                    .Enrich.WithProperty("Environment", environment)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                
            } else
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())    
                    .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                    .Build();
                
                Log.Information("elastic search added");
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/myapp.txt")
                    .Enrich.WithProperty("Environment", environment)
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
            }
        }

        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    if (env.IsDevelopment())
                    {
                        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                            // This one needs to be last
                            .AddEnvironmentVariables();
                    }
                    else if (env.IsStaging())
                    {
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                            .Build();
                    } else
                    {
                        config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                            .Build();
                    }
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}