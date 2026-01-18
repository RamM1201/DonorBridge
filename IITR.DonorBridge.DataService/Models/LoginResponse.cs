using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.DataService.Models
{
    public class LoginResponse
    {
        public int RegistrationID { get; set; }
        public string? Role { get; set; }
        public Token? Token { get; set; }
    }
}
