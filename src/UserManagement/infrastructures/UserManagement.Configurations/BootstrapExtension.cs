using Framework.Commands.CommandHandlers;
using Framework.Common;
using Framework.Domain.UnitOfWork;
using Framework.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Service.Contract;
using UserManagement.Persistance.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Service.Contract.AdminUserCommandHandlers;
using Service.Query;
using Service.Query.AdminUserQuery;
using UserManagement.Domains;

namespace UserManagement.Configurations
{
    public static class BootstrapExtension
    {
        public static IServiceCollection BootstrapCustomizeServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(TransactionalCommandHandlerMediatR<,>));
            // services.Scan(scan => scan
            //     .FromAssemblyOf<ChangedVerificationStatusUserDomainEventHandler>()
            //     .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            //     .AsImplementedInterfaces()
            //     .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<PersistGrantsRepository>()
                .AddClasses(classes => classes.AssignableTo<IRepository>())
                .AsMatchingInterface()
                .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<AddAdminUserCommandHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(ITransactionalCommandHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            services.Scan(scan => scan
                .FromAssemblyOf<GetAdminUserByIdQueryHandler>()
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandlerMediatR<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }
    }
}