using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.DomainModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HL.DAL.Model;
using System.Xml.Linq;

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
           
            PGUser PGUser = new PGUser();
            PGUser.PGAdminId = pGAdminDomain.PGAdminId;
            PGUser.Name = pGAdminDomain.Name;
            PGUser.Gender = pGAdminDomain.Gender;
            PGUser.Email = pGAdminDomain.Email;
            PGUser.PhoneNumber = pGAdminDomain.PhoneNumber;
            PGUser.Bulding_no = pGAdminDomain.Bulding_no;
            PGUser.Flour_no = pGAdminDomain.Flour_no;
            PGUser.Room_no = pGAdminDomain.Room_no;
            PGUser.Password = pGAdminDomain.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(pGAdminDomain.Password);
            PGUser.HashPassword = passwordHash;
            PGUser.Role_Id = 4;
            PGUser.Created_date = DateTime.Now.Date;
            _dataContextClass.SaveChanges();
            return Ok();
        }
    }
}
