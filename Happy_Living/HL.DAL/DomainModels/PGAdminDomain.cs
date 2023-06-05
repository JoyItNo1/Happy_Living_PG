using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.DomainModels
{
    public class PGAdminDomain
    {
        public int? PGAdminId { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Bulding_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
        public string? Password { get; set; }
        public int role_Id { get; set; }
    }
    public class PgWorkers
    {
        public int? PGAdminId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class WorkerInfo
    {
        public int PGWorks_Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
    public class PGUserInfo
    {
        public int PGUser_Id { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Bulding_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
        public string? Status { get; set; }
    }
    public class PGUserUpdate
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Bulding_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
    }
    public class AddRooms
    {
        public int? blockclas_No { get; set; }
        public List<Addfloor>? Addfloor { get; set; }
        public List<RoomsNo>? RoomsNo { get; set; }
        public List<Roomsharing>? Roomsharing { get; set; }
    }
    public class Addfloor
    {
        public int? block_no { get; set; }
        public int? Floor_No { get; set; }
    }
    public class RoomsNo
    {
        public int? Floor_No { get; set; }
        public int? Room_no { get; set; }
    }
    public class Roomsharing
    {
        public int? Rooms_No { get; set; }
        public String? Email { get; set; }
        public int? room_sharing { get; set; }
    }
    public class RoomInfo
    {
        public int? Building_No { get; set; }
        public int? floor_no { get; set; }
        public int? Room_no { get; set; }
        public int? room_sharing { get; set; }
        public string? Name { get; set; }

    }
    public class AdminInfo 
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        
    }
    public class AddStatus
    {
        public int PGUser_Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Bulding_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
        public string? Status { get; set; }
    }
    public class statuss
    {
        public string? status { get; set; }
    }
    public class Payment
    {
        public string UserId { get; set; }
        public string? PaymentsImage { get; set; }
    }
    public class Statusinfo
    {
        public DateTime? Date { get; set; }
        public string? Status { get; set;}
    }
    public class UserSuggetionCmpliantClass
    {
        public int? User_Id { get; set; }
        public string? User_name { get; set; }
        public string? SuggetionOrCmpliant { get; set; }
        public int? Block_no { get; set; }
        public int? Floor_no { get; set; }
        public int? Room_no { get; set; }
    }
}
