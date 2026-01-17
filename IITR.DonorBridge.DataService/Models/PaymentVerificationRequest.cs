using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class PaymentVerificationRequest
    {
        public string? RazorpayOrderId { get; set; }
        public string? RazorpayPaymentId { get; set; }
        public string? RazorpaySignature { get; set; }
    }
}
