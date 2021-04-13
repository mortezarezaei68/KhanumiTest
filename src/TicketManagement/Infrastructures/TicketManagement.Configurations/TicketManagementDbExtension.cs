using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Persistance.EF.Context;

namespace TicketManagement.Configurations
{
    public static class TicketManagementDbExtension
    {
        public static IServiceCollection TicketManagementDbInjections(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<TicketManagementDbContext>(b =>
                b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions => mySqlOptions.CommandTimeout(120)));

            return services;
        }
    }
}