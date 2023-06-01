using System.ComponentModel.DataAnnotations.Schema;

namespace HL.DAL.DomainModels
{
    public class LoginDomin
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class RegisterUser
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Usertype { get; set; }
        public string? Password { get; set; }
        public string? ComPassword { get; set; }
    }
    public class AdminRegisterPG
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Confirmpassword { get; set; }
        public string? Select_State { get; set; }
        public string? User_type { get; set; }
        public string? Select_City { get; set; }
        public string? Select_Area { get; set; }
        public string? PG_Name { get; set; }
        public string? PG_Location { get; set; }
        public DateTime? Payment_Methods { get; set; }
        public List<PgShering>? PgShering { get; set; }
        public List<PGType>? PGType { get; set; }
        public List<Imagepath>? Imagepath { get; set; }

    }
    public class PgShering
    {
        public string? SharingType { get; set; }
        public string? Price { get; set; }
    }
    public class PGType
    {
        public string? PGtype { get; set; }
    }
    public class ConfirmPassword
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPasswords { get; set; }
    }
    public class Imagepath
    {
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
    }
    public class SuperAdminRegister
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? Confirmpassword { get; set; }
    }
    public class Forgetpassword
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }

    }

}
