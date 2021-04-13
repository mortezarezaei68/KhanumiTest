using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Persistance.EF.Context;

namespace TicketManagement.Configurations
{
    public static class SeedData
    {
        public static async Task InitializeTicketManagementDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var serverDbContext = serviceScope.ServiceProvider.GetRequiredService<TicketManagementDbContext>();
            await serverDbContext.Database.MigrateAsync();

        }
    }
}