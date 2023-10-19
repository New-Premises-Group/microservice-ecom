﻿using IW.Common;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.PaymentDto
{
    public class CreatePayment
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int OrderID { get; set; }

        [Required]
        public Guid UserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [DefaultValue("Pending")]
        public PAYMENT_STATUS? Status { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [DefaultValue("VND")]
        public CURRENCY Currency { get; set; }
    }
}