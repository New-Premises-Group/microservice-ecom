using IW.Common;
using IW.Models.DTOs.Inventory;

namespace IW.Models.DTOs.TransactionDto
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public DateTime Date { get; set; }
        public TRANSACTION_TYPE Type { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
