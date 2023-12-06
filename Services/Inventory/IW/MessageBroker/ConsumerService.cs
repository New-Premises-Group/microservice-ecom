using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using IW.Interfaces;
using Newtonsoft.Json;
using IW.Common;
using IW.Models.DTOs.Inventory;
using IW.Services;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using IW.Exceptions.ReadInventoryError;
using IW.MessageBroker.MessageType;
using IW.MessageBroker.Exceptions;

namespace IW.MessageBroker
{
    public interface IConsumerService
    {
        Task ReadMessages();
    }

    internal class ConsumerService : IConsumerService, IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;
        private readonly IServiceProvider _services;

        const string _queueName = nameof(QUEUE_NAME.Order_Placed);
        public ConsumerService(IRabbitMqConsumer rabbitMqService, IServiceProvider services)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(queue: _queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            _services = services;
        }
        public virtual async Task ReadMessages()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = System.Text.Encoding.UTF8.GetString(body);
                var obj = JsonConvert.DeserializeObject<OrderCreatedMessage>(message);
                Console.WriteLine("Received Message: " + message);
                ProccessMessage(obj);
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Proccess incoming order created message.
        /// </summary>
        /// <param name="OrderCreatedMessage">A message created when an order is placed/created.</param>
        protected virtual async void ProccessMessage(OrderCreatedMessage message)
        {
            ICollection<InventoryDto> inventories = message.Items.Adapt<ICollection<InventoryDto>>();
            using (var scope = _services.CreateScope())
            {
                IInventoryService inventoryService=scope.ServiceProvider.GetRequiredService<IInventoryService>();
                try
                {
                    await inventoryService.UpdateStocks(inventories, TRANSACTION_TYPE.Sale);
                    IRabbitMqProducer producer = scope
                        .ServiceProvider
                        .GetRequiredService<IRabbitMqProducer>();
                    producer.Send(nameof(QUEUE_NAME.Order_Confirmed), message.Adapt<OrderConfirmedMessage>());
                }
                catch (InventoryNotFoundException ex)
                {

                }
                catch (OutOfStockException ex)
                {
                    IRabbitMqProducer producerPending = scope
                        .ServiceProvider
                        .GetRequiredService<IRabbitMqProducer>();

                    OrderPendingMessage pendingMessage=message.Adapt<OrderPendingMessage>();
                    pendingMessage.OutOfStockItem = ex.ProductId;
                    producerPending.Send(nameof(QUEUE_NAME.Order_Pending), pendingMessage);
                }
            }
            Console.WriteLine(message);
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
