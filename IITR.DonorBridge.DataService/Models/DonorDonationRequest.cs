using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class DonorDonationRequest
    {
        public int UserRegistrationId { get; set; }
        public int Amount { get; set; }
    }
}
