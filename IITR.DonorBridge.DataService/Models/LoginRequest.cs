using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class LoginRequest
    {
        public string? UserID { get; set; }
        public string? Password { get; set; }
    }
}
