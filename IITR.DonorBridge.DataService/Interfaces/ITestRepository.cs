using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using IITR.DonorBridge.DataService.Models;

namespace IITR.DonorBridge.DataService.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestModel>> GetAllUsersAsync();
    }
}
