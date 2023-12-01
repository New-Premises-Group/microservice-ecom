using IW.Models.Entities;

namespace IW.Models
{
    public class OrderCancelledMessage
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<ItemsDetailMessage>? Items { get; set; }

    }
}
