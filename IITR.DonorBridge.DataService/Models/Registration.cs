using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.DataService.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? State { get; set; }
        public DateOnly Created { get; set; }

    }
}
