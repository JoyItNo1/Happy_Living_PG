﻿using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.DomainModels;
using HL.DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Happy_Living.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly ILogin _login;

        public UserLoginController(ILogin login)
        {
            _login = login;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUser registerUser)
        {
            return _login.Register(registerUser);
        }

        [HttpPost]
        [Route("LogIn")]
        public IActionResult LogIn(LoginDomin loginDomin)
        {
            return _login.LogIn(loginDomin);
        }

        [HttpPost]
        [Route("validation")]
        public IActionResult validation(string? Email, string? phonenumber)
        {
            return _login.validation(Email, phonenumber);
        }

        [HttpPost]
        [Route("VerifyOTP")]
        public IActionResult VerifyOTP(string? email, string? PhoneNumber, string otp)
        {
            var result = _login.VerifyOTP(email, PhoneNumber, otp);
            if (result is OkObjectResult)
            {
                return Ok("Data Verified successful...!");
            }
            else
            {
                return result;
            }
        }

        [HttpPost]
        [Route("PGAdminRegistration")]
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG)
        {
            return _login.PGAdminRegistration(AdminRegisterPG);
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ConfirmPassword confirmPassword)
        {
            return _login.ChangePassword(confirmPassword);
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(Forgetpassword forgetpassword)
        {
            return _login.ForgotPassword(forgetpassword);
        }

        [HttpPost]
        [Route("SuperAdminRegister")]
        public IActionResult SuperAdminRegister(SuperAdminRegister SuperAdminRegister)
        {
            return _login.SuperAdminRegister(SuperAdminRegister);
        }

    }
}
