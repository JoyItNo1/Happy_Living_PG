using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class SuperAdminDomin
    {
        public int totaluser { get; set; }
        public int totalAdmin { get; set; } 
    }
    public class PGAdminData
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Select_State { get; set; }
        public string? Select_City { get; set; }
        public string? Select_Area { get; set; }
        public string? PG_Name { get; set; }
        public string? PG_Location { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? Created_date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime? Payment_Methods { get; set; }
    }
    public class UserInfo
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        public DateTime Created_date { get; set; }
    }
    public class SuperAdminInfo
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Created_date { get; set; }
    }
    public class SuperAdminRegister
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Confirmpassword { get; set; }
    }
}
