using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class AdminTransactionResponse
    {
        public int TransactionId { get; set; }
        public string? NameOfUser { get; set; }
        public int Amount { get; set; }
        public string? Status { get; set; }
        public DateOnly Updated { get; set; }
    }
}
