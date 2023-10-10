namespace IW.Interfaces
{
    public interface IRabbitMqProducer<T> where T : class
    {
        public void Send(string queueName,T message);
    }
}
