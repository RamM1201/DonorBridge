using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class TransactionStatusUpdateRequest
    {
        public string? OrderId { get; set; }
        public string? Status { get; set; }
        public string? PaymentId { get; set; }
    }
}
