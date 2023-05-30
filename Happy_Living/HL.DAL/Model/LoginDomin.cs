﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Model
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
}
