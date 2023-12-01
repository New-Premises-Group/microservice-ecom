namespace IW.Interfaces
{
    public interface IRabbitMqProducer
    {
        public void Send <TMessage>(string queueName, TMessage message);
    }
}
