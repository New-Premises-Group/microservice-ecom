using IW.Models.DTOs.Item;

namespace IW.Interfaces.Services
{
    public interface IMailService
    {
        Task Send(string receiverEmail, string name, ICollection<ItemDto> orderItems, decimal total);
    }
}
