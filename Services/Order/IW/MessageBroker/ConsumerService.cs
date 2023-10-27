using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using IW.Interfaces;
using Newtonsoft.Json;
using IW.Common;

namespace IW.MessageBroker
{
    public interface IConsumerService
    {
        Task ReadMessages();
    }

    internal class ConsumerService<T> : IConsumerService, IDisposable where T : class
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        const string _queueName = nameof(QUEUE_NAME.Order_Placed);
        public ConsumerService(IRabbitMqConsumer rabbitMqService)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue: _queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        }
        public virtual async Task ReadMessages()
        {
            ICollection<T> result = new List<T>();
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var obj = JsonConvert.DeserializeObject<T>(message);
                Console.WriteLine("Received Message: "+message);
                result.Add(obj);
                ProccessResult(result);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        protected virtual async void ProccessResult(ICollection<T> values)
        {
            //_unitOfWork.Items.AddRange(values);
            //await _unitOfWork.CompleteAsync();
            Console.WriteLine(values);
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
