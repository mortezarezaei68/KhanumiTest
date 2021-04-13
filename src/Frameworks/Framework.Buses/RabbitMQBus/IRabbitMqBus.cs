using System;
using Framework.Commands;

namespace Framework.Buses.RabbitMQBus
{
    public interface IRabbitMqBus:IDisposable
    {
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>(string exchangeName,string subscriberName)
            where T : Event
            where TH : IEventHandler<T>;
    }
}