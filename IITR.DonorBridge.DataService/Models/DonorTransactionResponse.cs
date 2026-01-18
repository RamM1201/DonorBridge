using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class DonorTransactionResponse
    {
        public int TransactionId { get; set; }
        public string? Status { get; set; }
        public DateOnly Updated { get; set; }
    }
}
