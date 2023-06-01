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
        public DataContextClass(DbContextOptions<DataContextClass> options) : base(options) { }
        public DbSet<RegisterClass> RegisterTable { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PGsheringType> PGsheringType { get; set; }
        public DbSet<OTPClass> OTPClass { get; set; }   
        public DbSet<PGTypes> PGTypes { get; set; }
        public DbSet<PGAdminRegister> PGAdminRegisters { get; set; }
        public DbSet<SuperAdminClass> SuperAdminClass { get; set; }
        public DbSet<ImageClass> ImageTable { get; set; }
        public DbSet<SelectedPGUser> SelectedPGUser { get; set; }
        public DbSet<PGUser> PGUserTable { get; set; }
    }
}
