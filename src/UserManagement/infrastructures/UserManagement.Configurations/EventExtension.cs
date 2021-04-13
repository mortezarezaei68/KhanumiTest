using System;
using Framework.Buses;
using Framework.Domain.UnitOfWork;
using Framework.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domains;
using UserManagement.Persistance.Context;
using UserManagement.Persistance.UnitOfWork;

namespace UserManagement.Configurations
{
    public static class EventExtension
    {
        public static IServiceCollection BootstrapEventBusServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IdentityUnitOfWork<,,>));
            services.AddScoped<IUnitOfWork,IdentityUnitOfWork<ApplicationDbContext,User,Role>>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IEventBus, EventBus>();
            return services;
        }
    }
}