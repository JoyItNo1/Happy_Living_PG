using HL.DAL.DomainModels;
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
        public IActionResult Register(RegisterUser registerUser);
        public IActionResult LogIn(LoginDomin loginDomin);
        public IActionResult validation(string? Email, string? phonenumber);
        public IActionResult VerifyOTP(string? email, string? PhoneNumber, string otp);
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG);
        public IActionResult ChangePassword(ConfirmPassword confirmPassword);
        public IActionResult SuperAdminRegister(SuperAdminRegister SuperAdminRegister);
        public IActionResult ForgotPassword(Forgetpassword forgetpassword);

    }
}
