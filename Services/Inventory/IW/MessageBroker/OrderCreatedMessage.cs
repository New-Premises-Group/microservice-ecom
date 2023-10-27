
namespace IW.Models
{
    public class OrderCreatedMessage
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ItemsDetailMessage>? Items { get; set; }

    }
    public class ItemsDetailMessage
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
