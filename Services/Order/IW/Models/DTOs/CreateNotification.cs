using IW.Interfaces.Commands;

namespace IW.Models.DTOs
{
    public class CreateNotification:IRequest
    {
        public OrderCreatedMessage Message { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public decimal Total { get; set; }
        public ICollection<CreateItem> Items { get; set; }
    }
}
