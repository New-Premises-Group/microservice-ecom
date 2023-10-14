

using IW.Models.Entities;

namespace IW.Models.DTOs.Cart
{ 
    public class CartDto
    {
        public int? Id { get; set; }
        public Guid? UserId { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
