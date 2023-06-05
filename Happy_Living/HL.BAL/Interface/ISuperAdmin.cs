using HL.DAL.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.BAL.Interface
{
    public interface ISuperAdmin
    {
        
        public IActionResult GetByDashboard();
        public IEnumerable<PGAdminData> PGAdminData();
        public IActionResult DeleteAdmin(int Id);
        public IActionResult ActiveOrDeactive(int Id, bool? Stetus);
        public IEnumerable<UserInfo> UserData();
        public IEnumerable<SuperAdminInfo> SuperAdminInfo(int? Id);
        public IActionResult AddPGToUser(SelectedPgForUser selectedPGUser);
        public IEnumerable<Userinfo> UserInfo(int? Id);
        public IActionResult ActiveInactive(int[]? Id, bool? IS_Active);
    }
}
