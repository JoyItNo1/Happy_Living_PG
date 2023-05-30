using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HL.BAL.Implementation
{
    public class UserRegister : ControllerBase, ILogin
    {
        private readonly DataContextClass _dataContextClass;
        private readonly IConfiguration _configuration;
        public UserRegister(DataContextClass dataContextClass , IConfiguration configuration) 
        {
            _dataContextClass = dataContextClass;
            _configuration = configuration;
        }
        public  IActionResult Register(RegisterUser registerUser)
        { 
            if(registerUser ==null)
                return NotFound();
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (registerUser.Email == "" || !Regex.IsMatch(registerUser.Email, email))
            {
                return BadRequest("Invalied Email");
            }
            string Passwordpattern = "^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?])[A-Za-z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{8,}$";
            if (!Regex.IsMatch(registerUser.Password, Passwordpattern))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (registerUser.Password != registerUser.ComPassword)
            {
                return BadRequest("Password is not Matching");
            }

            var Details =  _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == registerUser.Email || i.PhoneNumber == registerUser.PhoneNumber);
            if(Details != null )
                return BadRequest("Data Already exists");
            RegisterClass reg =new RegisterClass();

            reg.Name = registerUser.Name;
            reg.PhoneNumber = registerUser.PhoneNumber;
            reg.Email = registerUser.Email;
            reg.Gender = registerUser.Gender;
            reg.Password = registerUser.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            reg.HashPassword = passwordHash;
            reg.ComPassword = registerUser.ComPassword;
            reg.OTP ="";
            var Role = _dataContextClass.UserTypes.FirstOrDefault(e => e.Usertype == registerUser.Usertype);
            if (Role.Usertype.ToLower() == "pgadmin")
            {
                reg.role_Id = 1;
            }
            else
            {
                reg.role_Id = 2;
            }
            _dataContextClass.RegisterTable.Add(reg);
             _dataContextClass.SaveChanges();
            return Ok("User Registered..!");
        }
        public IActionResult LogIn(string? Email,string?  PhoneNumber ,string? Password)
        {
            if (Email == null && PhoneNumber == null)
                return NotFound();
            var Details = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == Email || i.PhoneNumber == PhoneNumber);
            if (Details == null) 
                return NotFound();
            string storedPasswordHash = Details.HashPassword;
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(Password, storedPasswordHash);
            if (!isPasswordCorrect)
            {
                return NotFound("Password Not Found");
            }
            if (PhoneNumber != null) {

                List<Claim> claims1 = new List<Claim>
                {

                        new Claim(ClaimTypes.SerialNumber, PhoneNumber)
                };
                var newKey1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
                var creds1 = new SigningCredentials(newKey1, SecurityAlgorithms.HmacSha512Signature);
                var token1 = new JwtSecurityToken(claims: claims1, expires: DateTime.Now.AddDays(1), signingCredentials: creds1);
                var y1 = _dataContextClass.RegisterTable.FirstOrDefault(e => e.Email == Email || e.PhoneNumber == PhoneNumber);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token1),
                    expiration = token1.ValidTo,
                    Employee_Id = y1.Uid,
                    Role_Id = y1.role_Id,
                });
            }
            List<Claim> claims = new List<Claim>
            {

                        new Claim(ClaimTypes.Email, Email)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
            var y = _dataContextClass.RegisterTable.FirstOrDefault(e => e.Email == Email || e.PhoneNumber == PhoneNumber);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                Employee_Id = y.Uid,
                Role_Id = y.role_Id,
            });
        }

        public IActionResult Add_UserType(UserType userType)
        {
            var data = _dataContextClass.UserTypes.FirstOrDefault(i => i.Usertype == userType.Usertype);
            if (data != null)
            {
                return BadRequest("User type Already Existes...!");
            }
            UserType Sd=new UserType();
            Sd.Usertype = userType.Usertype;
            _dataContextClass.UserTypes.Add(Sd);
            _dataContextClass.SaveChanges();
            return Ok("User Type Added..!");
        }
        public IActionResult validation(string? Email, string? phonenumber)
        {
            if (Email == null && phonenumber == null)
            {
                return BadRequest("Please enter data...!");
            }

            var details = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == Email || i.PhoneNumber == phonenumber);
            details.OTP = "";
            _dataContextClass.SaveChanges();

            Random random = new Random();

            string otp = (random.Next(1000, 9999)).ToString();
           
                if (details == null)
                {
                    return BadRequest("Please provid valied Mail Or Phone Number");
                }
                if (Email == null && phonenumber != null)
                {
                    // Your Account SID and Auth Token from twilio.com/console
                    string accountSid = "AC6e58d36390a5ec00be0016b2d424e99f";
                    string authToken = "c94e935541ed3f3b83f3712e1a48852e";

                    // Initialize the Twilio client
                    TwilioClient.Init(accountSid, authToken);

                    // Send an SMS message
                    var message1 = MessageResource.Create(
                        body: $"Your otp is:{otp} Do not shere with any one...!",
                        from: new Twilio.Types.PhoneNumber("+12707173050"), // Twilio phone number
                        to: new Twilio.Types.PhoneNumber("+916363112696") // recipient's phone number
                    );
                    details.OTP = otp;
                    _dataContextClass.SaveChanges();
                    return Ok($"OTP generated and sent to Phone Number: '{phonenumber}'");
                }
                details.OTP = otp;
                _dataContextClass.SaveChanges();

                // Send the OTP to the user via email 
                var fullname = details.Name;
                string fromAddress = "Joyitsolutions1@gmail.com";
                string Password = "rpcfydphzeoafsig";
                string toAddress = details.Email;
                string emailHeader = "<html><body><h1>OTP to Reset Password</h1></body></html>";
                string emailFooter = $"<html><head><title>JoyItsolutions</title></head><body><p>Hi {fullname}, <br> This is the confidential email. Don't share your otp with anyone..!<br>  </p></body></html>";
                string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your one time password(otp) is: <h3>{details.OTP}</h3></p></body></html>";
                string emailContent = emailHeader + emailBody + emailFooter;
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromAddress);
                message.Subject = "Reset Password";
                message.To.Add(new MailAddress(toAddress));
                message.Body = emailContent;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, Password),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
                return Ok($"OTP generated and sent to MailID: '{details.Email}'");
        }
        public IActionResult VerifyOTP(string? email, string? PhoneNumber, string? otp)
        {
            var details = _dataContextClass.RegisterTable.FirstOrDefault(x => x.Email == email || x.PhoneNumber==PhoneNumber && x.OTP == otp);
            if(details == null)
            {
                return NotFound();
            }
            details.OTP = ""; 
            _dataContextClass.SaveChanges();
            return Ok("Date varified...!");
        }
    }
}
