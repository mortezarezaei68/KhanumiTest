using System;
using System.IO;
using Framework.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TicketManagement.Persistance.EF.Context
{
    public class TicketManagementDbContextFactory:IDesignTimeDbContextFactory<TicketManagementDbContext>
    {
        public TicketManagementDbContext CreateDbContext(string[] args)
        {
            IHttpContextAccessor accessor = new HttpContextAccessor();
            ICurrentUser currentUser = new CurrentUser(accessor);
            var config = GetAppSetting();
            var optionsBuilder = new DbContextOptionsBuilder<TicketManagementDbContext>();
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                return new TicketManagementDbContext(optionsBuilder.Options,currentUser);

        }

        
        private IConfigurationRoot GetAppSetting()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;
            var isStaging = environment == Environments.Staging;

            if (isDevelopment)
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())    
                    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                    .Build();
            if (isStaging)
                return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())    
                    .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                    .Build();

            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}