using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Common
{
    public static class RabbitMqExtension
    {
        public static IServiceCollection RabbitMqConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });
            return services;
        }
    }
}