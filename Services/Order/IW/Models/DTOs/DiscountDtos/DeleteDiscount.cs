using IW.Interfaces.Commands;

namespace IW.Models.DTOs.DiscountDtos
{
    public class DeleteDiscount:IRequest
    {
        public int Id { get; set; }
    }
}
