using IW.Common;

namespace IW.Models.DTOs.TransactionDto
{
    public class GetTransaction
    {
        public int? InventoryId { get; set; }
        public DateTime? Date { get; set; }
        public TRANSACTION_TYPE? Type { get; set; }
    }
}

