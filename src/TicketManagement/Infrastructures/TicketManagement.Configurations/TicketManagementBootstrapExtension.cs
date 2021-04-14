
using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Domain.UnitOfWork;
using Framework.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TicketManagement.CommandHandler;
using TicketManagement.Domain;
using TicketManagement.Persistance.EF.Repository;
using TicketManagement.Queries;

namespace TicketManagement.Configurations
{
    public static class TicketManagementBootstrapExtension
    {
        public static IServiceCollection BootstrapTicketManagement(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(TransactionalCommandHandlerMediatR<,>));
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.Scan(scan => scan
                .FromAssemblyOf<AddTicketCommandHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(ITransactionalCommandHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<GetAllTicketsQueryHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}