using HL.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.BAL.Interface
{
    public interface ILogin
    {
        IActionResult Register(RegisterUser registerUser);
        IActionResult LogIn(string? Email, string? PhoneNumber, string? Password);
        IActionResult validation(string? Email, string? phonenumber);
        public IActionResult Add_UserType(UserType userType);
        IActionResult VerifyOTP(string? email, string? PhoneNumber, string otp);
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG);
        public IActionResult ChangePassword(ConfirmPassword confirmPassword);
        public IActionResult SuperAdminRegister(SuperAdminRegister SuperAdminRegister);

    }
}
