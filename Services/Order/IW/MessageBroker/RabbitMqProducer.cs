using IW.Configurations;
using IW.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace IW.MessageBroker
{
    public class RabbitMqProducer<T> : IRabbitMqProducer<T> where T : class
    {
        readonly ConnectionFactory _connectionFactory;
        private readonly RabbitMqOptions _options;
        public RabbitMqProducer(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
            _connectionFactory = new ConnectionFactory()
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
            };
        }
        public void Send(string queueName,T message)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            // Declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            // Put the data on to the product queue
            channel.BasicPublish(exchange: String.Empty, routingKey: queueName, body: body);
        }
    }
}
