using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.DomainModels;
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
        public IEnumerable<SuperAdminInfo> SuperAdminInfo (int? Id)
        {
            var data = from a in _dataContextClass.SuperAdminClass
                       where (a.SuperAdmin_id == Id)
                       select new SuperAdminInfo
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                           Created_date = a.Created_date,
                       };
            return data.ToList();
        }
        //User Add PG to user
        public IActionResult AddPGToUser (SelectedPgForUser selectedPGUser)
        {
            var data = _dataContextClass.SelectedPGUser.FirstOrDefault(s => s.Select_PG == selectedPGUser.Select_PG);
            if (data == null)
            {
                return BadRequest(" PG is not available..!");
            }
            var T = new SelectedPGUser();
            T.PGAdminId = data.PGAdminId;
            T.Select_State = selectedPGUser.Select_State;
            T.Select_PG = selectedPGUser.Select_PG;
            T.Select_City = selectedPGUser.Select_City;
            T.Select_Area = selectedPGUser.Select_Area;
            T.Select_PG_Type = selectedPGUser.Select_PG_Type;
            T.Location = selectedPGUser.Location;
            T.Sharing_Type = selectedPGUser.Sharing_Type;
            T.Sharing = selectedPGUser.Sharing;
            T.Cost = selectedPGUser.Cost;
            _dataContextClass.SelectedPGUser.Add(T);
            _dataContextClass.SaveChanges();
            var data1 = _dataContextClass.PGAdminRegisters.FirstOrDefault(s => s.PG_Name == selectedPGUser.Select_PG);
            return Ok(new
            {
                AdminName = data1.Name,
                Number = data1.PhoneNumber
            });
        }
        public IEnumerable<Userinfo> UserInfo(int? Id)
        {
            var data = from a in _dataContextClass.RegisterTable
                       where (a.Uid == Id)
                       select new Userinfo
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                       };
            return data.ToList();
        }
    }
}
