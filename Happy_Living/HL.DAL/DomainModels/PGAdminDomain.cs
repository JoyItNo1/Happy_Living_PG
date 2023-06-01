using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.DomainModels
{
    public class PGAdminDomain
    {
        public int? PGAdminId { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Bulding_no { get; set; }
        public string? Flour_no { get; set; }
        public string? Room_no { get; set; }
        public string? Password { get; set; }
        public int role_Id { get; set; }
    }
}
