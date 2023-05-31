using HL.BAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Happy_Living.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase 
    {
        private readonly ISuperAdmin _superAdmin;

        public SuperAdminController(ISuperAdmin superAdmin) 
        {
            _superAdmin =  superAdmin;
        }
        [HttpGet]
        [Route("DashBord")]
        public IActionResult GetByDashboard()
        {
            return _superAdmin.GetByDashboard();
        }

    }
}
