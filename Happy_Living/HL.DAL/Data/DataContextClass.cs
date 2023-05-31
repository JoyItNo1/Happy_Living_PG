using HL.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL.DAL.Data
{
    public class DataContextClass : DbContext
    {
        public DataContextClass(DbContextOptions<DataContextClass> options) : base(options)
        {
        }
        public DbSet<RegisterClass> RegisterTable { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public DbSet<OTPClass> OTPClass { get; set; }   
    }
}
