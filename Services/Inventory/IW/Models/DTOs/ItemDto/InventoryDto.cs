
namespace IW.Models.DTOs.Inventory
{ 
    public class InventoryDto
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public bool? Availability { get; set; }
        public int? Quantity {  get; set; }
    }
}
