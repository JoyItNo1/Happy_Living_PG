using HL.BAL.Interface;
using HL.DAL.DomainModels;
using HL.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Happy_Living.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PGAdminController : ControllerBase
    {
        private readonly IPGAdmin _PGAdmin;

        public PGAdminController(IPGAdmin PGAdmin)
        {
            _PGAdmin = PGAdmin;
        }

        [HttpPost]
        [Route("PGUserAdd")]
        public IActionResult AddPGUser(PGAdminDomain pGAdminDomain)
        {
            return _PGAdmin.AddPGUser(pGAdminDomain);
        }

        [HttpPost]
        [Route("AddPgWorkers")]
        public IActionResult AddPgWorkers(PgWorkers pgWorkers)
        {
            return _PGAdmin.AddPgWorkers(pgWorkers);
        }

        [HttpDelete]
        [Route("DeleteWorker")]
        public IActionResult DeleteWorker(int Id)
        {
            return _PGAdmin.DeleteWorker(Id);
        }

        [HttpPut]
        [Route("AddPgWorkersUpadate")]
        public IActionResult AddPgWorkersUpadate(PgWorkers pgWorkers)
        {
            return _PGAdmin.AddPgWorkersUpadate(pgWorkers);
        }

        [HttpGet]
        [Route("workerInfos")]
        public IEnumerable<WorkerInfo> workerInfos()
        {
            return _PGAdmin.workerInfos();
        }

        [HttpGet]
        [Route("Userinfo")]
        public IEnumerable<PGUserInfo> Userinfo()
        {
            return _PGAdmin.Userinfo();
        }

        [HttpPut]
        [Route("UpdatePGUser")]
        public IActionResult UpdatePGUser(PGUserUpdate pGAdminDomain)
        {
            return _PGAdmin.UpdatePGUser(pGAdminDomain);
        }

        [HttpDelete]
        [Route("DeletePGUser")]
        public IActionResult DeletePGUser(int Id)
        {
            return _PGAdmin.DeletePGUser(Id);
        }
        [HttpGet]
        [Route("RoomInfo")]
        public IEnumerable<RoomInfo> RoomInfo()
        {
            return _PGAdmin.RoomInfo();
        }

        [HttpGet]
        [Route("UserSuggetionCmpletInfo")]
        public IEnumerable<UserSuggetionCmplet> UserSuggetionCmpletInfo()
        {
            return _PGAdmin.UserSuggetionCmpletInfo();
        }

        [HttpGet]
        [Route("AdminInfo")]
        public IEnumerable<AdminInfo> AdminInfo(int? Id)
        {
            return _PGAdmin.AdminInfo(Id);
        }
        [HttpGet]
        [Route("DashbordUser")]
        public IEnumerable<stetussss> DashbordUser(int Id)
        {
            return _PGAdmin.DashbordUser(Id);
        }
        [HttpPost]
        [Route("AddMonthStetus")]
        public IActionResult AddMonthStetus()
        {
            return _PGAdmin.AddMonthStetus();
        }

        [HttpPut]
        [Route("UpdateMonthStetus")]
        public IActionResult UpdateMonthStetus(AddStetus addStetus)
        {
            return _PGAdmin.UpdateMonthStetus(addStetus);
        }

        [HttpGet]
        [Route("StetusAll")]
        public IEnumerable<Stetusinfo> StetusAll(int? Id)
        {
            return _PGAdmin.StetusAll(Id);
        }

        [HttpPost]
        [Route("SuggestionOrCompliant")]
        public IActionResult SuggestionOrCompliant(UserSuggetionCmpletClass userSuggetionCmpletClass)
        {
            return _PGAdmin.SuggestionOrCompliant(userSuggetionCmpletClass);
        }

        [HttpPost]
        [Route("Addpayment")]
        public IActionResult Addpayment(Payment paymentImage)
        {
            return _PGAdmin.Addpayment(paymentImage);
        }
    }
}
