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
        public IActionResult AddPgWorkersUpadate(PgWorkers pgWorkers);
        public IEnumerable<WorkerInfo> workerInfos();
        public IActionResult AddPgWorkers(PgWorkers pgWorkers);
        public IEnumerable<PGUserInfo> Userinfo();
        public IActionResult UpdatePGUser(PGUserUpdate pGAdminDomain);
        public IActionResult DeletePGUser(int Id);
        public IEnumerable<RoomInfo> RoomInfo();
        public IEnumerable<UserSuggetionCmplet> UserSuggetionCmpletInfo();
        public IEnumerable<AdminInfo> AdminInfo(int? Id);
        public IEnumerable<stetussss> DashbordUser(int Id);
        public IActionResult AddMonthStetus();
        public IActionResult UpdateMonthStetus(AddStetus addStetus);
        public IEnumerable<Stetusinfo> StetusAll(int? Id);
        public IActionResult SuggestionOrCompliant(UserSuggetionCmpletClass userSuggetionCmpletClass);
        public IActionResult Addpayment(Payment paymentImage);
    }
}
