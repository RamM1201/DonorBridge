using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class DonorDonationResponse
    {
        public int DonationId { get; set; }
        public int Amount { get; set; }
        public string? Status { get; set; }
        public DateOnly Updated { get; set; }
    }
}
