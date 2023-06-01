using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.BAL.Implementation
{
    public class PGAdminRepo : ControllerBase , IPGAdmin
    {
        private readonly DataContextClass _dataContextClass;

        public PGAdminRepo(DataContextClass dataContextClass)
        {
            _dataContextClass = dataContextClass;
        }
        public IActionResult AddPGUser(PGAdminDomain pGAdminDomain)
        {
            var data = _dataContextClass.PGUserTable.FirstOrDefault(b => b.Email== pGAdminDomain.Email ||b.PhoneNumber == pGAdminDomain.PhoneNumber);
            if (data != null)
                return BadRequest("User Already Added...!");

            return Ok();
        }
    }
}
