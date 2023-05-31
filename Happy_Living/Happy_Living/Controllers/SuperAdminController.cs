using HL.BAL.Interface;
using HL.DAL.Model;
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

        [HttpGet]
        [Route("PGAdminData")]
        public IEnumerable<PGAdminData> PGAdminData()
        {
            return _superAdmin.PGAdminData();
        }

        [HttpDelete]
        [Route("DeleteAdmin")]
        public IActionResult DeleteAdmin(int Id)
        {
            return _superAdmin.DeleteAdmin(Id);
        }

        [HttpPut]
        [Route("ActiveOrDeactive")]
        public IActionResult ActiveOrDeactive(int Id, bool? Stetus)
        {
            return _superAdmin.ActiveOrDeactive(Id, Stetus);
        }

        [HttpGet]
        [Route("UserInfo")]
        public IEnumerable<UserInfo> UserData()
        {
            return _superAdmin.UserData();
        }

        [HttpGet]
        [Route("SuperAdminInfo")]
        public IEnumerable<SuperAdminInfo> SuperAdminInfo(string? Email)
        {
            return _superAdmin.SuperAdminInfo(Email);
        }

    }
}
