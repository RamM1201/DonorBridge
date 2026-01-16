using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponse?> GetLoginAsync(LoginRequest request);
    }
}
