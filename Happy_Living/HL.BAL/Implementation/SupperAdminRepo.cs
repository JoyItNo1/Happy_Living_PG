using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HL.BAL.Implementation
{
    public class SupperAdminRepo : ControllerBase, ISuperAdmin
    {
        private readonly DataContextClass _dataContextClass;

        public SupperAdminRepo(DataContextClass dataContextClass) 
        {
            _dataContextClass = dataContextClass;
        }
        //Dash Bord
        public IActionResult GetByDashboard()
        {
            var UserCount = _dataContextClass.RegisterTable.Count();
            var AdminCount = _dataContextClass.PGAdminRegisters.Count();
            return Ok(new
            {
                User = UserCount,
                Admin = AdminCount,
            });
        }
        //PG Admin Information
        public IEnumerable<PGAdminData> PGAdminData()
        {
        var data = from a in _dataContextClass.PGAdminRegisters
                       select new PGAdminData
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           Select_State = a.Select_State,
                           Select_City = a.Select_City,
                           Select_Area = a.Select_Area,
                           PG_Name = a.PG_Name,
                           PG_Location = a.PG_Location,
                           Created_date = a.Created_date,
                           Payment_Methods = a.Payment_Methods
                       };
            return data.ToList();
        }
        //Delete PG Admin
        public IActionResult DeleteAdmin(int Id)
        {
            var data = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.PGAdmin_Id == Id);
            if(data==null)
            {
                return BadRequest("No data Found");
            }
            _dataContextClass.PGAdminRegisters.Remove(data);
            _dataContextClass.SaveChanges();
            return Ok("Admin Deleted");
        }
        //Active/Deactive PG Admin
        public IActionResult ActiveOrDeactive(int Id ,bool? Stetus)
        {
            var data = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.PGAdmin_Id == Id);
            if (data == null)
            {
                return BadRequest("No data Found");
            }
            if (Stetus == true)
            {
                data.Is_Auth = true;
                _dataContextClass.SaveChanges();
                return Ok("Updeted..!");
            }
            else
            {
                data.Is_Auth = false;
                _dataContextClass.SaveChanges();
                return Ok("Updeted..!");
            }
        }
        //User Information
        public IEnumerable<UserInfo> UserData()
        {
            var data = from a in _dataContextClass.RegisterTable
                       select new UserInfo
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           Gender = a.Gender,
                           Created_date = a.Created_date,
                       };
            return data.ToList();
        }
        //Supper Admin Info
        public IEnumerable<SuperAdminInfo> SuperAdminInfo (string? Email)
        {
            var data = from a in _dataContextClass.RegisterTable
                       where (a.Email == Email)
                       select new SuperAdminInfo
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           Created_date = a.Created_date,
                       };
            return data.ToList();

        }
    }
}
