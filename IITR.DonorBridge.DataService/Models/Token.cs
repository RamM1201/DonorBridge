using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Models
{
    public class Token
    {
        public string? AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
