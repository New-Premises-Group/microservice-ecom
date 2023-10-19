using IW.Common;

namespace IW.Models.DTOs.PaymentDto
{
    public class PaymentDto
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
        public PAYMENT_STATUS? Status { get; set; }
        public decimal Amount { get; set; }
        public CURRENCY Currency { get; set; }
    }
}