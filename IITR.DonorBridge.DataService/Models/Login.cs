using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.DataService.Models
{
    public class Login
    {
        public int LoginID { get; set; }
        public int RegistrationID { get; set; }
        public string? UserID { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
