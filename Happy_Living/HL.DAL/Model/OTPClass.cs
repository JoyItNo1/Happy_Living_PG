using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
{
    public class OTPClass
    {
        public int Id { get; set; } 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OTP { get; set; }
    }
}
