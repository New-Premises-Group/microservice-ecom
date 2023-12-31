﻿using IW.Common;
using IW.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace IW.Models.DTOs.TransactionDto
{
    public class CreateTransaction
    {
        [Required]
        public int InventoryId { get; set; }
        [Required]
        public Entities.Inventory Inventory { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TRANSACTION_TYPE Type { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
