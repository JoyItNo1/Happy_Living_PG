using HL.BAL.Interface;
using HL.DAL.Data;
using HL.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
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
        //user register
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
            var Role = _dataContextClass.UserTypes.FirstOrDefault(e => e.Usertype == registerUser.Usertype);
            if (Role.Usertype.ToLower() == "User")
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
        //user Login
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
        //Add User type like user and Pg Admin
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
        //Verify Email and Number
        public IActionResult validation(string? Email, string? phonenumber)
        {
            if (Email == null && phonenumber == null)
            {
                return BadRequest("Please enter data...!");
            }
            var data = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == Email && i.PhoneNumber == phonenumber);
            if (data != null)
            {
                return BadRequest("User Already Exists..!");
            }
            OTPClass reg = new OTPClass();
            Random random = new Random();

            string otp = (random.Next(1000, 9999)).ToString();
            var details = _dataContextClass.OTPClass.FirstOrDefault(h => h.Email == Email || h.PhoneNumber==phonenumber);
           

            if (Email == null && phonenumber != null)
            {
                string pattern = @"^\d{10}$";
                if (!Regex.IsMatch(phonenumber, pattern))
                {
                    return BadRequest("Invalied Phone number");
                }
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
                if (details.PhoneNumber != null)
                {
                    details.PhoneNumber = phonenumber;
                    details.OTP = otp;
                    _dataContextClass.SaveChanges();
                }
                else
                {
                    reg.PhoneNumber = phonenumber;
                    reg.OTP = otp;
                    _dataContextClass.OTPClass.Add(reg);
                    _dataContextClass.SaveChanges();
                }
                return Ok($"OTP generated and sent to Phone Number: '{phonenumber}'");
            }
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(Email, email))
            {
                return BadRequest("Invalied Email");
            }
            if (details.Email != null)
            {
                details.Email = Email;
                details.OTP = otp;
                _dataContextClass.SaveChanges();
            }
            else
            {
                reg.Email = Email;
                reg.PhoneNumber = phonenumber;
                reg.OTP = otp;
                _dataContextClass.OTPClass.Add(reg);
                _dataContextClass.SaveChanges();
            }

                // Send the OTP to the user via email 
                var fullname = "Your Mail Successfully Verified...!";
                string fromAddress = "Joyitsolutions1@gmail.com";
                string Password = "rbmxrfjnujzssmyf";
                string toAddress = Email;
                string emailHeader = "<html><body><h1>OTP to  verify</h1></body></html>";
                string emailFooter = $"<html><head><title>JoyItsolutions</title></head><body><p>Hi {fullname}, <br> This is the confidential email. Don't share your otp with anyone..!<br>  </p></body></html>";
                string emailBody = $"<html><head><title>Don't replay this Mail</title></head><body><p>Your one time password(otp) is: <h3>{otp}</h3></p></body></html>";
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
                return Ok($"OTP generated and sent to MailID: '{Email}'");
        }
        public IActionResult VerifyOTP(string? email, string? PhoneNumber, string? otp)
        {
            var data=_dataContextClass.OTPClass.FirstOrDefault(x=> x.Email == email || x.PhoneNumber == PhoneNumber && x.OTP == otp);
            if(data==null)
            {
                return NotFound();
            }
            data.OTP = ""; 
            _dataContextClass.SaveChanges();
            return Ok("Date varified...!");
        }
        //Add PG Admin
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG)
        {
            var data = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == AdminRegisterPG.Email && i.PhoneNumber == AdminRegisterPG.PhoneNumber);
            if(data!=null)
            {
                return BadRequest("User Already Exists..!");
            }

            if (data == null)
            {
                var TS = new PGAdminRegister();
                TS.Name = AdminRegisterPG.Name;
                TS.Email = AdminRegisterPG.Email;
                TS.PhoneNumber = AdminRegisterPG.PhoneNumber;
                TS.Created_date = DateTime.Now.Date;
                TS.Is_Auth = true;
                TS.Password = AdminRegisterPG.Password;
                TS.Confirmpassword = AdminRegisterPG.Confirmpassword;
                TS.Payment_Methods = AdminRegisterPG.Payment_Methods;
                TS.PG_Name = AdminRegisterPG.PG_Name;
                TS.PG_Location = AdminRegisterPG.PG_Location;
                TS.Select_Area = AdminRegisterPG.Select_Area;
                TS.Select_City = AdminRegisterPG.Select_City;
                TS.Select_State = AdminRegisterPG.Select_State;
                var Role = _dataContextClass.UserTypes.FirstOrDefault(e => e.Usertype == AdminRegisterPG.User_type);
                if (Role.Usertype.ToLower() == "pgadmin")
                {
                    TS.Role_Id = 1;
                }
                else
                {
                    TS.Role_Id = 2;
                }
                _dataContextClass.PGAdminRegisters.Add(TS);
                _dataContextClass.SaveChanges();
                int lastsummaryid = _dataContextClass.PGAdminRegisters.Max(item => item.PGAdmin_Id);


                foreach (var s in AdminRegisterPG.PgShering)
                {
                    var T = new PGsheringType();
                    T.SharingType = s.SharingType;
                    T.Price = s.Price;
                    T.PGAdminId = lastsummaryid;
                    _dataContextClass.PGsheringType.Add(T);
                    _dataContextClass.SaveChanges();
                }

                foreach (var s in AdminRegisterPG.PGType)
                {
                    var T = new PGTypes();
                    T.PGtype = s.PGtype;
                    T.PGAdminId = lastsummaryid;
                    _dataContextClass.PGTypes.Add(T);
                    _dataContextClass.SaveChanges();
                }
                return Ok("PG Admin Added...!");
            }
            else
            {
                return BadRequest("PG Admin already Submited...!");
            }
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

    }
}
