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
        [Route("AddPgWorkersUpdate")]
        public IActionResult AddPgWorkersUpdate(PgWorkers pgWorkers)
        {
            return _PGAdmin.AddPgWorkersUpdate(pgWorkers);
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
        [Route("UserSuggetionCmpliantInfo")]
        public IEnumerable<UserSuggetionCmpliant> UserSuggetionCmpliantInfo()
        {
            return _PGAdmin.UserSuggetionCmpliantInfo();
        }

        [HttpGet]
        [Route("AdminInfo")]
        public IEnumerable<AdminInfo> AdminInfo(int? Id)
        {
            return _PGAdmin.AdminInfo(Id);
        }
        [HttpGet]
        [Route("DashbordUser")]
        public IEnumerable<statuss> DashbordUser(int Id)
        {
            return _PGAdmin.DashbordUser(Id);
        }
        [HttpPost]
        [Route("AddMonthStatus")]
        public IActionResult AddMonthStatus()
        {
            return _PGAdmin.AddMonthStatus();
        }

        [HttpPut]
        [Route("UpdateMonthStatus")]
        public IActionResult UpdateMonthStatus(AddStatus addStatus)
        {
            return _PGAdmin.UpdateMonthStatus(addStatus);
        }

        [HttpGet]
        [Route("StatusAll")]
        public IEnumerable<Statusinfo> StatusAll(int? Id)
        {
            return _PGAdmin.StatusAll(Id);
        }

        [HttpPost]
        [Route("SuggestionOrCompliant")]
        public IActionResult SuggestionOrCompliant(UserSuggetionCmpliantClass userSuggetionCmpliantClass)
        {
            return _PGAdmin.SuggestionOrCompliant(userSuggetionCmpliantClass);
        }

        [HttpPost]
        [Route("Addpayment")]
        public IActionResult Addpayment(Payment paymentImage)
        {
            return _PGAdmin.Addpayment(paymentImage);
        }

        [HttpPost]
        [Route("AddRoom")]
        public IActionResult AddRoom(AddRooms addRooms)
        {
            return _PGAdmin.AddRoom(addRooms);
        }
       
    }
}
