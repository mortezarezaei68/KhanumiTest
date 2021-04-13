using System;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Domains;
using UserManagement.Persistance;
using UserManagement.Persistance.Context;

namespace UserManagement.Configurations
{
    public static class IdentityDbExtension
    {
        public static IServiceCollection IdentityDbInjections(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(b =>
                b.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    mySqlOptions => mySqlOptions.CommandTimeout(120)));
            
            services.AddIdentity<User,Role>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                } )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }
    }
}