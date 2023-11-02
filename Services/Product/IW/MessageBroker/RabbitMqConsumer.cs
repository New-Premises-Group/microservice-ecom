using IW.Configurations;
using IW.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace IW.MessageBroker
{
    internal sealed class RabbitMqConsumer : IRabbitMqConsumer 
    {
        private readonly RabbitMqOptions _options;
        public RabbitMqConsumer(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }
        public IConnection CreateChannel()
        {
            ConnectionFactory connection = new()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                DispatchConsumersAsync = true
            };
            var channel = connection.CreateConnection();
            return channel;
        }
    }
}
