using IITR.DonorBridge.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.DataService.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login?> GetLoginDetailsAsync(string userId, string password);
    }
}
