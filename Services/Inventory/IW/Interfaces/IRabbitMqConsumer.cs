using RabbitMQ.Client;

namespace IW.Interfaces
{
    public interface IRabbitMqConsumer
    {
        IConnection CreateChannel();
    }
}
