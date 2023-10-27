using IW.Common;

namespace IW.Models.DTOs.PaymentDto
{
    public class UpdatePayment
    {
        public int? OrderID { get; set; }
        public PAYMENT_STATUS? Status { get; set; }
        public decimal? Amount { get; set; }
        public CURRENCY Currency { get; set; }
    }
}