using System;
using Framework.Buses;
using Framework.Buses.RabbitMQBus;
using Framework.Domain.UnitOfWork;
using Framework.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Persistance.EF.Context;

namespace TicketManagement.Configurations
{
    public static class TicketManagementEventExtension
    {
        public static IServiceCollection BootstrapEventTicketManagement(this IServiceCollection services)
        {
            services.AddTransient(typeof(UnitOfWork<TicketManagementDbContext>));
            services.AddScoped<IUnitOfWork,UnitOfWork<TicketManagementDbContext>>();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IEventBus, EventBus>();
            services.AddScoped<IRabbitMqBus,RabbitMqBus>();
            return services;    
        }
    }
}