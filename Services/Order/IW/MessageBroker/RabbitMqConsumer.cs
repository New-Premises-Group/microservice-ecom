using IW.Configurations;
using IW.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace IW.MessageBroker
{
    internal sealed class RabbitMqConsumer<T> : IRabbitMqConsumer where T : class
    {
        private readonly RabbitMqOptions _options;
        public RabbitMqConsumer(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new ConnectionFactory()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
            };
            connection.DispatchConsumersAsync = true;
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
