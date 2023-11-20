namespace IW.MessageBroker.MessageType
{
    public class OrderPendingMessage
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int OutOfStockItem { get; set; }
        public ICollection<ItemsDetailMessage>? Items { get; set; }
    }
}
