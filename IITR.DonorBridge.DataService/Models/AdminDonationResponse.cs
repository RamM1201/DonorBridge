using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class AdminDonationResponse
    {
        public int DonationId { get; set; }
        public int UserRegistrationId { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public string? Status { get; set; }
        public string? State { get; set; }
        public DateOnly Updated { get; set; }
    }
}
