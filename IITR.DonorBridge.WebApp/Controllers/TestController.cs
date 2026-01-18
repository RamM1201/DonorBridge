using IITR.DonorBridge.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IITR.DonorBridge.WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly TestService _testservice;
        public TestController(TestService testservice)
        {
            _testservice = testservice;
        }
        // GET: TestController
        public async Task<ActionResult> Index()
        {
            return View(await _testservice.GetDataAsync());
        }

    }
}
