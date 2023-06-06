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
using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;

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
            bool isTableEmpty = !_dataContextClass.Room_Sharing.Any();
            if (isTableEmpty)
            {
                return BadRequest("No Rooms Available..!");
            }
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
            var RSds = _dataContextClass.Room_Sharing.FirstOrDefault(l => l.Rooms_No == pGAdminDomain.Room_no && l.Email != pGAdminDomain.Email);

            if (RSds != null)
            {
                if (RSds.Email == "")
                {
                    RSds.Email = pGAdminDomain.Email;
                    _dataContextClass.SaveChanges();
                }
                else
                {
                    var RSDSs = _dataContextClass.Room_Sharing.Count(l => l.Rooms_No == pGAdminDomain.Room_no);
                    if(RSds.room_sharing != RSDSs)
                    {
                        var T = new Room_Sharing();
                        T.Rooms_No = pGAdminDomain.Room_no;
                        T.Email = pGAdminDomain.Email;
                        T.room_sharing = RSds.room_sharing;
                        _dataContextClass.Room_Sharing.Add(T);
                        _dataContextClass.SaveChanges();
                    }
                    else
                    {
                        return BadRequest("Room Already Filled");
                    }
                }
            }
            _dataContextClass.PGUserTable.Add(PGUser);
            _dataContextClass.SaveChanges();
            return Ok("User Added..!");
        }
        public IActionResult AddPgWorkers(PgWorkers pgWorkers)
        {
            var data = _dataContextClass.PGWorks.FirstOrDefault(k => k.PhoneNumber == pgWorkers.PhoneNumber);
            if (data != null)
            {
                return BadRequest("Worker Already existed..!");
            }
            PGWorks S = new PGWorks();

            S.PGAdminId = pgWorkers.PGAdminId;
            S.Name = pgWorkers.Name;
            S.Gender = pgWorkers.Gender;
            S.PhoneNumber = pgWorkers.PhoneNumber;
            S.Created_date = DateTime.Now.Date;
            _dataContextClass.PGWorks.Add(S);
            _dataContextClass.SaveChanges();    
            return Ok("Worker Add..!");
        }
        public IEnumerable<WorkerInfo> workerInfos()
        {
            var data = from a in _dataContextClass.PGWorks
                       select new WorkerInfo
                       {
                           PGWorks_Id = a.PGWorks_Id,
                           Name = a.Name,
                           PhoneNumber = a.PhoneNumber,
                           Gender= a.Gender
                       };
            return data.ToList();
        }
        public IActionResult AddPgWorkersUpadate(PgWorkers pgWorkers)
        {
            var data = _dataContextClass.PGWorks.FirstOrDefault(k => k.PGAdminId == pgWorkers.PGAdminId);
            if (data == null)
            {
                return BadRequest("Worker not existed..!");
            }
            data.PGAdminId = pgWorkers.PGAdminId;
            data.Name = pgWorkers.Name;
            data.Gender = pgWorkers.Gender;
            _dataContextClass.SaveChanges();
            return Ok("Worker Updated..!");
        }

        public IActionResult DeleteWorker(int Id)
        {
            var data = _dataContextClass.PGWorks.FirstOrDefault(k => k.PGWorks_Id == Id);
            if (data == null)
            {
                return BadRequest("Worker not existed..!");
            }
            _dataContextClass.PGWorks.Remove(data);
            _dataContextClass.SaveChanges();
            return Ok("Worker Deleted..!");
        }

        public IEnumerable<PGUserInfo> Userinfo()
        {
            var data = from a in _dataContextClass.PGUserTable
                       join g in _dataContextClass.Stetus
                       on a.PGUser_Id equals g.PGUser_Id
                       select new PGUserInfo
                       {
                           PGUser_Id = a.PGUser_Id,
                           Name = a.Name,
                           PhoneNumber = a.PhoneNumber,
                           Gender = a.Gender,
                           Email = a.Email,
                           Bulding_no = a.Bulding_no,
                           Flour_no = a.Flour_no,
                           Room_no = a.Room_no,
                           Stetus = g.Stetuss,
                       };
            return data.ToList();
        }
        public IActionResult UpdatePGUser(PGUserUpdate pGAdminDomain)
        {
            var data = _dataContextClass.PGUserTable.FirstOrDefault(b => b.Email == pGAdminDomain.Email || b.PhoneNumber == pGAdminDomain.PhoneNumber);
            if (data == null)
                return BadRequest("User Not Exist...!");

            data.Name = pGAdminDomain.Name;
            data.Gender = pGAdminDomain.Gender;
            data.Email = pGAdminDomain.Email;
            data.PhoneNumber = pGAdminDomain.PhoneNumber;
            data.Bulding_no = pGAdminDomain.Bulding_no;
            data.Flour_no = pGAdminDomain.Flour_no;
            data.Room_no = pGAdminDomain.Room_no;
            var Bds = _dataContextClass.building_blockclas.FirstOrDefault(l => l.blockclas_No == pGAdminDomain.Bulding_no);
            if (Bds != null)
            {
                var Fds = _dataContextClass.Flore.FirstOrDefault(l => l.Flore_No == pGAdminDomain.Flour_no);
                if (Fds != null)
                {
                    var Rds = _dataContextClass.Rooms.FirstOrDefault(l => l.Room_no == pGAdminDomain.Room_no);
                    if (Rds != null)
                    {
                        var RSds = _dataContextClass.Room_Sharing.FirstOrDefault(l => l.Rooms_No == Rds.Room_no);
                        if (RSds != null)
                        {
                            RSds.Email = data.Email;
                            _dataContextClass.SaveChanges();
                        }
                        else
                        {
                            return BadRequest("No beds available..!");
                        }
                    }
                }
            }
            _dataContextClass.SaveChanges();
            return Ok("User Updated..!");
        }
        public IActionResult DeletePGUser(int Id)
        {
            var data = _dataContextClass.PGUserTable.FirstOrDefault(b => b.PGUser_Id == Id);
            if (data != null)
                return BadRequest("User Not Exist...!");
            var Bds = _dataContextClass.building_blockclas.FirstOrDefault(l => l.blockclas_No == data.Bulding_no);
            if (Bds != null)
            {
                var Fds = _dataContextClass.Flore.FirstOrDefault(l => l.Flore_No == data.Flour_no);
                if (Fds != null)
                {
                    var Rds = _dataContextClass.Rooms.FirstOrDefault(l => l.Room_no == data.Room_no);
                    if (Rds != null)
                    {
                        var RSds = _dataContextClass.Room_Sharing.FirstOrDefault(l => l.Rooms_No == Rds.Room_no || l.Email == data.Email);
                        if (RSds != null)
                        {
                            _dataContextClass.Room_Sharing.Remove(RSds);
                            _dataContextClass.SaveChanges();
                        }
                    }
                }
            } 
            _dataContextClass.PGUserTable.Remove(data);
            _dataContextClass.SaveChanges();
            return Ok("User Deleted..!");
        }
        public IActionResult AddRoom(AddRooms addRooms)
        {
            if (addRooms == null)
            {
                return BadRequest("Please enter some data...!");
            }
            var f = _dataContextClass.building_blockclas.FirstOrDefault(p => p.blockclas_No == addRooms.blockclas_No);
            if (f != null)
            {
                f.blockclas_No = addRooms.blockclas_No;
                _dataContextClass.SaveChanges();
            }
            else
            {
                var TS = new building_blockclas();
                TS.blockclas_No = addRooms.blockclas_No;
                _dataContextClass.building_blockclas.Add(TS);
                _dataContextClass.SaveChanges();
            }
            //int lastsummaryid = _dataContextClass.building_blockclas.Max(item => item.building_blockclas_Id);


            foreach (var s in addRooms.Addfloure)
            {
                var T = new Flore();
                T.block_no = addRooms.blockclas_No;
                T.Flore_No = s.Flore_No;

                _dataContextClass.Flore.Add(T);
                _dataContextClass.SaveChanges();
            }
            int lastsummaryid1 = _dataContextClass.Flore.Max(item => item.Flore_Id);

            foreach (var s in addRooms.RoomsNo)
            {
                var data = _dataContextClass.Rooms.FirstOrDefault(o => o.Room_no == s.Room_no);
                if (data != null)
                {
                    return BadRequest("Room Already Entered");
                }
                var Td = new Rooms();
                Td.Flore_No = s.Flore_No;
                Td.Room_no = s.Room_no;
                _dataContextClass.Rooms.Add(Td);
                _dataContextClass.SaveChanges();
            }
            int lastsummaryid11 = _dataContextClass.Rooms.Max(item => item.Rooms_Id);

            foreach (var s in addRooms.Roomshareing)
            {
                var T = new Room_Sharing();
                T.Rooms_No = s.Rooms_No;
                T.Email = "";
                T.room_sharing = s.room_sharing;
                _dataContextClass.Room_Sharing.Add(T);
                _dataContextClass.SaveChanges();
            }
            return Ok("PG Rooms Added...!");
        }
        public IEnumerable<RoomInfo> RoomInfo()
        {
            var data = from a in _dataContextClass.building_blockclas
                       join f in _dataContextClass.Flore
                        on a.building_blockclas_Id equals f.block_no
                       join d in _dataContextClass.Rooms
                       on f.Flore_Id equals d.Flore_No
                       join g in _dataContextClass.Room_Sharing
                       on d.Room_no equals g.Rooms_No
                       join h in _dataContextClass.PGUserTable
                       on g.Email equals h.Email
                       select new RoomInfo
                       {
                           Building_No = a.blockclas_No,
                           flore_no = f.Flore_No,
                           Room_no = d.Room_no,
                           room_sharing = g.room_sharing,
                           Name = h.Name,

                       };
            return data.ToList();
        }
        public IEnumerable<AdminInfo> AdminInfo(int? Id)
        {
            var data = from a in _dataContextClass.PGAdminRegisters
                       where(a.PGAdmin_Id== Id)
                       select new AdminInfo
                       {
                           Name = a.Name,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber,
                       };
            return data.ToList();
        }
        public IEnumerable<UserSuggetionCmplet> UserSuggetionCmpletInfo()
        {
            var data = from a in _dataContextClass.UserSuggetionCmplet
                       select new UserSuggetionCmplet
                       {
                           User_Id = a.User_Id,
                           User_name = a.User_name,
                           SuggetionOrCmplet = a.SuggetionOrCmplet,
                           Block_no = a.Block_no,
                           Flour_no = a.Flour_no,
                           Room_no = a.Room_no,
                           Created_date = a.Created_date,
                       };
            return data.ToList();
        }
        public IActionResult AddMonthStetus()
        {
            var data = _dataContextClass.PGUserTable.ToList();
            foreach (var s in data)
            {

                var T = new Stetus();
                T.PGUser_Id = s.PGUser_Id;
                T.Name = s.Name;
                T.Flour_no = s.Flour_no;
                T.Room_no = s.Room_no;
                T.Created_date = DateTime.Now.Date;
                T.Bulding_no = s.Bulding_no;
                T.PhoneNumber = s.PhoneNumber;
                T.Email = s.Email;
                T.Stetuss = "Pending";
                _dataContextClass.Stetus.Add(T);
                _dataContextClass.SaveChanges();
            }
            return Ok("Monthly Status Added..!");
        }
        public IActionResult UpdateMonthStetus(AddStetus addStetus)
        {
            var data = _dataContextClass.Stetus.FirstOrDefault(o => o.PGUser_Id == addStetus.PGUser_Id);
            if (data == null)
            {
                return NotFound();
            }
            data.Stetuss = addStetus.Stetus;
            _dataContextClass.SaveChanges();
            return Ok("Record Updated...!");
        }
        public IEnumerable<stetussss> DashbordUser(int Id)
        {
            var data = from a in _dataContextClass.Stetus
                       where (a.PGUser_Id == Id)
                       select new stetussss
                       {
                           stetus = a.Stetuss
                       };
            return data.ToList();
        }
        public IActionResult Addpayment(Payment paymentImage)
        {
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var data =  _dataContextClass.PaymentImage.FirstOrDefault(t =>
                t.UserId == paymentImage.UserId && t.Created_date.Month == currentMonth);
            if(data != null)
            {
                return BadRequest("Already Sent Payment...!");
            }
            PaymentImage B = new PaymentImage();
            B.PaymentsImage = paymentImage.PaymentsImage;
            B.Created_date = currentDate;
            B.UserId = paymentImage.UserId;
            _dataContextClass.PaymentImage.Add(B);
            _dataContextClass.SaveChanges();
            return Ok("Payment Details Sent...!");
        }
        public IEnumerable<Stetusinfo> StetusAll(int? Id)
        {
            var data = from a in _dataContextClass.Stetus
                       join f in _dataContextClass.PGUserTable
                        on a.PGUser_Id equals f.PGUser_Id
                       where (f.PGUser_Id == Id)
                       select new Stetusinfo
                       {
                           Date = a.Created_date,
                           Stetus = a.Stetuss
                       };
            return data.ToList();
        }
        public IActionResult SuggestionOrCompliant(UserSuggetionCmpletClass userSuggetionCmpletClass)
        {
            if (userSuggetionCmpletClass == null)
            {
                return BadRequest("Enter Data...!");
            }
            UserSuggetionCmplet user = new UserSuggetionCmplet();
            user.User_Id = userSuggetionCmpletClass.User_Id;
            user.User_name = userSuggetionCmpletClass.User_name;
            user.SuggetionOrCmplet = userSuggetionCmpletClass.SuggetionOrCmplet;
            user.Block_no = userSuggetionCmpletClass.Block_no;
            user.Flour_no = userSuggetionCmpletClass.Flour_no;
            user.Room_no = userSuggetionCmpletClass.Room_no;
            user.Created_date = DateTime.Now;
            _dataContextClass.UserSuggetionCmplet.Add(user);
            _dataContextClass.SaveChanges();
            return Ok("Message Sent...!");
        }
    }
}
