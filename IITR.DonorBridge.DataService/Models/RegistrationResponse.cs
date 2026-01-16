using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.DataService.Models
{
    public class RegistrationResponse
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? State { get; set; }
        public DateOnly Created { get; set; }
        public bool isVerified { get; set; }

    }
}
