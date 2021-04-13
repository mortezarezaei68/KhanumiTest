using System;
using System.Text;
using Framework.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Framework.Buses.RabbitMQBus
{
    public class RabbitMqBus:IRabbitMqBus
    {
        private bool _disposed;

        private readonly IConnection _connection;

        private IModel _channel;
        private IModel Model
        {
            get
            {
                if (_channel == null)
                    _channel = _connection.CreateModel();

                return _channel;
            }
        }

        private readonly IServiceProvider _provider;


        public RabbitMqBus(IConfiguration config, IServiceProvider provider)
        {
            _provider = provider;

            var connectionFactory = new ConnectionFactory
            {
                HostName = config["EventBus:HostName"],
                Port = int.Parse(config["EventBus:Port"]),
                UserName = config["EventBus:UserName"],
                Password = config["EventBus:Password"],
                DispatchConsumersAsync = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
            };

            _connection = connectionFactory.CreateConnection();
        }
        public void Publish<T>(T @event) where T : Event
        {
            CreateExchangeIfNotExists(@event.ExchangeName);

            var jsonMessage = JsonConvert.SerializeObject(@event);
            var bytesMessage = Encoding.UTF8.GetBytes(jsonMessage);

            Model.BasicPublish(@event.ExchangeName, string.Empty, body: bytesMessage);
        }
        

        private void CreateExchangeIfNotExists(string exchangeName)
        {
            Model.ExchangeDeclare(exchangeName, ExchangeType.Fanout, true);
        }

        public void Subscribe<T, TH>(string exchangeName,string subscriberName)
            where T : Event
            where TH : IEventHandler<T>
        {
            BindQueue(exchangeName, subscriberName);

            var consumer = new AsyncEventingBasicConsumer(Model);
            
            consumer.Received += async (obj, args) =>
            {
                using (var scope = _provider.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<T>>();
                    var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());
                    var message = JsonConvert.DeserializeObject<T>(jsonMessage);

                    await handler.Handle(message);

                    Model.BasicAck(args.DeliveryTag, false);
                }
            };

            Model.BasicConsume(string.Empty, false, consumer);
        }
        private void BindQueue(string exchangeName, string subscriberName)
        {
            CreateExchangeIfNotExists(exchangeName);

            Model.QueueDeclare(subscriberName, true, false, autoDelete: false);
            Model.QueueBind(subscriberName, exchangeName, "");
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _channel?.Dispose();
                _connection?.Dispose();
            }

            _disposed = true;
        }




        
    }
}