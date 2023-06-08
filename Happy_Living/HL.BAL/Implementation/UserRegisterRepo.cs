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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using HL.DAL.DomainModels;

namespace HL.BAL.Implementation
{
    public class UserRegisterRepo : ControllerBase, ILogin
    {
        private readonly DataContextClass _dataContextClass;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UserRegisterRepo(DataContextClass dataContextClass , IConfiguration configuration , IWebHostEnvironment hostEnvironment) 
        {
            _dataContextClass = dataContextClass;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }
        //user register
        public  IActionResult Register(RegisterUser registerUser)
        { 
            if(registerUser ==null)
                return NotFound();
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (registerUser.Email == "" || !Regex.IsMatch(registerUser.Email, email))
            {
                return BadRequest("Invalid Email");
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
            var data = _dataContextClass.SuperAdminClass.FirstOrDefault(i => i.Email == registerUser.Email && i.PhoneNumber == registerUser.PhoneNumber);
            var data1 = _dataContextClass.PGUserTable.FirstOrDefault(i => i.Email == registerUser.Email && i.PhoneNumber == registerUser.PhoneNumber);
            var data11 = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == registerUser.Email && i.PhoneNumber == registerUser.PhoneNumber);
            if (Details!=null || data != null || data1 != null || data11 != null)
                return BadRequest("User Mail or phone number Already Added enter other data...!");
            RegisterClass reg =new RegisterClass();

            reg.Name = registerUser.Name;
            reg.PhoneNumber = registerUser.PhoneNumber;
            reg.Email = registerUser.Email;
            reg.Gender = registerUser.Gender;
            reg.Password = registerUser.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
            reg.HashPassword = passwordHash;
            var Role = _dataContextClass.UserTypes.FirstOrDefault(e => e.Usertype == registerUser.Usertype);
            if (Role.Usertype.ToLower() == "User")
            {
                reg.role_Id = 2;
            }
            _dataContextClass.RegisterTable.Add(reg);
             _dataContextClass.SaveChanges();
            return Ok("User Registered..!");
        }
        //user Login
        public IActionResult LogIn(LoginDomin loginDomin)
        {
            if (loginDomin.Email == null)
                return NotFound();
            var Details = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == loginDomin.Email);//|| i.PhoneNumber == PhoneNumber);
            if (Details == null)
            {
                var Details121 = _dataContextClass.PGUserTable.FirstOrDefault(i => i.Email == loginDomin.Email);// i.PhoneNumber == PhoneNumber);
                if (Details121 == null)
                {
                    var Details1211 = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == loginDomin.Email);// i.PhoneNumber == PhoneNumber);
                    if (Details1211 == null)
                    {
                        var Details12111 = _dataContextClass.SuperAdminClass.FirstOrDefault(i => i.Email == loginDomin.Email);// i.PhoneNumber == PhoneNumber);
                        if (Details12111 == null)
                            return NotFound();
                        string storedPasswordHash12111 = Details12111.Hashpassword;
                        bool isPasswordCorrect12111 = BCrypt.Net.BCrypt.Verify(loginDomin.Password, storedPasswordHash12111);
                        if (!isPasswordCorrect12111)
                        {
                            return NotFound("Password Not Found");
                        }
                        List<Claim> claims12351 = new List<Claim>
                        {
                           new Claim(ClaimTypes.Email, loginDomin.Email)
                        };
                        var newKey12351 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        _configuration.GetSection("AppSettings:Token").Value!));
                        var creds12351 = new SigningCredentials(newKey12351, SecurityAlgorithms.HmacSha512Signature);
                        var token12351 = new JwtSecurityToken(claims: claims12351, expires: DateTime.Now.AddDays(1), signingCredentials: creds12351);
                        var y12351 = _dataContextClass.SuperAdminClass.FirstOrDefault(e => e.Email == loginDomin.Email);// || e.PhoneNumber == PhoneNumber);
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token12351),
                            expiration = token12351.ValidTo,
                            Admin_Id = y12351.SuperAdmin_id,
                            Role_Id = y12351.Role_Id,
                        });
                    }
                    string storedPasswordHash1213 = Details1211.Hashpassword;
                    bool isPasswordCorrect12113 = BCrypt.Net.BCrypt.Verify(loginDomin.Password, storedPasswordHash1213);
                    if (!isPasswordCorrect12113)
                    {
                        return NotFound("Password Not Found");
                    }

                    List<Claim> claims12353 = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, loginDomin.Email)
                    };
                    var newKey12353 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value!));
                    var creds12353 = new SigningCredentials(newKey12353, SecurityAlgorithms.HmacSha512Signature);
                    var token12353 = new JwtSecurityToken(claims: claims12353, expires: DateTime.Now.AddDays(1), signingCredentials: creds12353);
                    var y12353 = _dataContextClass.PGAdminRegisters.FirstOrDefault(e => e.Email == loginDomin.Email);// || e.PhoneNumber == PhoneNumber);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token12353),
                        expiration = token12353.ValidTo,
                        Admin_Id = y12353.PGAdmin_Id,
                        Role_Id = y12353.Role_Id,
                    });
                }
                string storedPasswordHash1211 = Details121.HashPassword;
                bool isPasswordCorrect1211 = BCrypt.Net.BCrypt.Verify(loginDomin.Password, storedPasswordHash1211);
                if (!isPasswordCorrect1211)
                {
                    return NotFound("Password Not Found");
                }

                List<Claim> claims1235 = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, loginDomin.Email)
                    };
                var newKey1235 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
                var creds1235 = new SigningCredentials(newKey1235, SecurityAlgorithms.HmacSha512Signature);
                var token1235 = new JwtSecurityToken(claims: claims1235, expires: DateTime.Now.AddDays(1), signingCredentials: creds1235);
                var y1235 = _dataContextClass.PGUserTable.FirstOrDefault(e => e.Email == loginDomin.Email);// || e.PhoneNumber == PhoneNumber);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token1235),
                    expiration = token1235.ValidTo,
                    Admin_Id = y1235.PGUser_Id,
                    Role_Id = y1235.Role_Id,
                });
            }
            string storedPasswordHash = Details.HashPassword;
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(loginDomin.Password, storedPasswordHash);
            if (!isPasswordCorrect)
            {
                return NotFound("Password Not Found");
            }
            List<Claim> claims = new List<Claim>
            {

                        new Claim(ClaimTypes.Email, loginDomin.Email)
            };
            var newKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(newKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
                var y = _dataContextClass.RegisterTable.FirstOrDefault(e => e.Email == loginDomin.Email);// || e.PhoneNumber == PhoneNumber);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                Admin_Id = y.Uid,
                Role_Id = y.role_Id,
            });
        }
        //Add User type like user and Pg Admin
        public IActionResult Add_UserType(UserType userType)
        {
            var data = _dataContextClass.UserTypes.FirstOrDefault(i => i.Usertype == userType.Usertype);
            if (data != null)
            {
                return BadRequest("User type Already Exists...!");
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
                    return BadRequest("Invalid Phone number");
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
                return BadRequest("Invalid Email");
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
            return Ok("Date verified...!");
        }
        //Add PG Admin
        public IActionResult PGAdminRegistration(AdminRegisterPG AdminRegisterPG)
        {
            
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (AdminRegisterPG.Email == "" || !Regex.IsMatch(AdminRegisterPG.Email, email))
            {
                return BadRequest("Invalid Email");
            }
            string Passwordpattern = "^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?])[A-Za-z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{8,}$";
            if (!Regex.IsMatch(AdminRegisterPG.Password, Passwordpattern))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (AdminRegisterPG.Password != AdminRegisterPG.Confirmpassword)
            {
                return BadRequest("Password is not Matching");
            }
            var Details = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == AdminRegisterPG.Email || i.PhoneNumber == AdminRegisterPG.PhoneNumber);
            var data = _dataContextClass.SuperAdminClass.FirstOrDefault(i => i.Email == AdminRegisterPG.Email && i.PhoneNumber == AdminRegisterPG.PhoneNumber);
            var data1 = _dataContextClass.PGUserTable.FirstOrDefault(i => i.Email == AdminRegisterPG.Email && i.PhoneNumber == AdminRegisterPG.PhoneNumber);
            var data11 = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == AdminRegisterPG.Email && i.PhoneNumber == AdminRegisterPG.PhoneNumber);
            if (Details != null || data != null || data1 != null || data11 != null)
            {
                return BadRequest("User Mail or phone number Already Added enter other data...!");
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
                TS.Payment_Methods = AdminRegisterPG.Payment_Methods;
                TS.PG_Name = AdminRegisterPG.PG_Name;
                TS.PG_Location = AdminRegisterPG.PG_Location;
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(AdminRegisterPG.Password);
                TS.Hashpassword = passwordHash;
                TS.Select_Area = AdminRegisterPG.Select_Area;
                TS.Select_City = AdminRegisterPG.Select_City;
                TS.Select_State = AdminRegisterPG.Select_State;
                var Role = _dataContextClass.UserTypes.FirstOrDefault(e => e.Usertype == AdminRegisterPG.User_type);
                if (Role.Usertype.ToLower() == "pgadmin")
                {
                    TS.Role_Id = 3;
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

                foreach(var s in AdminRegisterPG.Imagepath)
                {
                    var T = new ImageClass();
                    T.PGAdminId = lastsummaryid;
                    T.Image1 = s.Image1;
                    T.Image2 = s.Image2;
                    T.Image3 = s.Image3;
                    T.Image4 = s.Image4;
                    _dataContextClass.ImageTable.Add(T);
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
                return BadRequest("PG Admin already Submitted...!");
            }
        }
        public IActionResult ChangePassword(ConfirmPassword confirmPassword)
        {
            var data = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == confirmPassword.Email || i.PhoneNumber == confirmPassword.PhoneNumber);
            if (data == null)
            {
                return BadRequest("No Data Found");
            }
            data.Password = confirmPassword.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(confirmPassword.Password);
            data.HashPassword = passwordHash;
            _dataContextClass.SaveChanges();
            return Ok("Data Updated");
        }
        public IActionResult SuperAdminRegister(SuperAdminRegister SuperAdminRegister)
        {
            string email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (SuperAdminRegister.Email == "" || !Regex.IsMatch(SuperAdminRegister.Email, email))
            {
                return BadRequest("Invalid Email");
            }
            string Passwordpattern = "^(?=.*[A-Z])(?=.*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?])[A-Za-z0-9!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>\\/?]{8,}$";
            if (!Regex.IsMatch(SuperAdminRegister.Password, Passwordpattern))
            {
                return BadRequest("Password should contain first letter should capital letter and one special symbol");
            }
            if (SuperAdminRegister.Password != SuperAdminRegister.Confirmpassword)
            {
                return BadRequest("Password is not Matching");
            }
            var Details = _dataContextClass.RegisterTable.FirstOrDefault(i => i.Email == SuperAdminRegister.Email || i.PhoneNumber == SuperAdminRegister.PhoneNumber);
            var data = _dataContextClass.SuperAdminClass.FirstOrDefault(i => i.Email == SuperAdminRegister.Email && i.PhoneNumber == SuperAdminRegister.PhoneNumber);
            var data1 = _dataContextClass.PGUserTable.FirstOrDefault(i => i.Email == SuperAdminRegister.Email && i.PhoneNumber == SuperAdminRegister.PhoneNumber);
            var data11 = _dataContextClass.PGAdminRegisters.FirstOrDefault(i => i.Email == SuperAdminRegister.Email && i.PhoneNumber == SuperAdminRegister.PhoneNumber);
            if (Details != null || data != null || data1 !=null || data11 != null)
            {
                return BadRequest("User Mail or phone number Already Added enter other data...!");
            }
            if (data == null)
            {
                var TS = new SuperAdminClass();
                TS.Name = SuperAdminRegister.Name;
                TS.Email = SuperAdminRegister.Email;
                TS.PhoneNumber = SuperAdminRegister.PhoneNumber;
                TS.Created_date = DateTime.Now.Date;
                TS.Password = SuperAdminRegister.Password;
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(SuperAdminRegister.Password);
                TS.Hashpassword = passwordHash;
                TS.Role_Id = 1;
                _dataContextClass.SuperAdminClass.Add(TS);
                _dataContextClass.SaveChanges();
            }
            return Ok("User Registered..!");
        }
        //Upload Images
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("Image file is not selected");
            }
            var folderPath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imagePath = Path.Combine("Images", fileName);
            return Ok(new { imagePath });
        }


        //Upload Images Path
        public IActionResult GetImage(string imagePath)
        {
            var fullPath = Path.Combine(_hostEnvironment.ContentRootPath, imagePath);
            var imageBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(imageBytes, "image/jpeg");
        }
        public IActionResult ForgotPassword(Forgetpassword forgetpassword)
        {
            var data = _dataContextClass.PGAdminRegisters.FirstOrDefault(x => x.Email == forgetpassword.Email || x.PhoneNumber == forgetpassword.PhoneNumber);
            if (data == null)
            {
                return BadRequest("User Not Exists..!");
            }
            data.Password = forgetpassword.Password;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(forgetpassword.Password);
            data.Hashpassword = passwordHash;
            _dataContextClass.SaveChanges();
            return Ok("Password Updated..!");
        }
    }
  
}
