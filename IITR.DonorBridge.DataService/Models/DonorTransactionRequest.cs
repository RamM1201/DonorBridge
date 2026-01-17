using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class DonorTransactionRequest
    {
        public int DonationId { get; set; }
        public string? OrderId { get; set; }
    }
}
