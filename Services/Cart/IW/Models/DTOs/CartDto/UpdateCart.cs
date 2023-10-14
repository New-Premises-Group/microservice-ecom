
using IW.Models.Entities;

namespace IW.Models.DTOs.Cart
{
    public class UpdateCart
    {
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
