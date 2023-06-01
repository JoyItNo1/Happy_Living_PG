using HL.DAL.DomainModels;
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
        IActionResult AddPGUser(PGAdminDomain pGAdminDomain);
    }
}
