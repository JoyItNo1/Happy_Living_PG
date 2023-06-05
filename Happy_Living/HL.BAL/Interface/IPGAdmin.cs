using HL.DAL.DomainModels;
using HL.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.BAL.Interface
{
    public interface IPGAdmin
    {
        public IActionResult AddPGUser(PGAdminDomain pGAdminDomain);
        public IActionResult DeleteWorker(int Id);
        public IActionResult AddPgWorkersUpdate(PgWorkers pgWorkers);
        public IEnumerable<WorkerInfo> workerInfos();
        public IActionResult AddPgWorkers(PgWorkers pgWorkers);
        public IEnumerable<PGUserInfo> Userinfo();
        public IActionResult UpdatePGUser(PGUserUpdate pGAdminDomain);
        public IActionResult DeletePGUser(int Id);
        public IEnumerable<RoomInfo> RoomInfo();
        public IEnumerable<UserSuggetionCmpliant> UserSuggetionCmpliantInfo();
        public IEnumerable<AdminInfo> AdminInfo(int? Id);
        public IEnumerable<statuss> DashbordUser(int Id);
        public IActionResult AddMonthStatus();
        public IActionResult UpdateMonthStatus(AddStatus addStatus);
        public IEnumerable<Statusinfo> StatusAll(int? Id);
        public IActionResult SuggestionOrCompliant(UserSuggetionCmpliantClass userSuggetionCmpliantClass);
        public IActionResult Addpayment(Payment paymentImage);
        public IActionResult AddRoom(AddRooms addRooms);
    }
}
