using HL.BAL.Interface;
using HL.DAL.Data;
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
        public IActionResult LogIn(string? Email, string? PhoneNumber, string? Password)
        {
            return _login.LogIn(Email, PhoneNumber, Password);
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
        [Route("Add_UserType")]
        public IActionResult Add_UserType(UserType userType)
        {
            return _login.Add_UserType(userType);
        }
        [HttpPost]
        [Route("PGAdminRegistration")]
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG)
        {
            return _login.PGAdminRegistration(AdminRegisterPG);
        }
        [HttpPost]
        [Route("SuperAdminRegister")]
        public IActionResult SuperAdminRegister(SuperAdminRegister SuperAdminRegister)
        {
            return _login.SuperAdminRegister(SuperAdminRegister);
        }
    }
}
