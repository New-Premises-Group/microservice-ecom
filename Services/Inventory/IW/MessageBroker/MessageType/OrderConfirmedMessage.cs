namespace IW.MessageBroker.MessageType
{
    public class OrderConfirmedMessage
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }
}
