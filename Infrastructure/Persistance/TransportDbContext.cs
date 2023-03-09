using Domain;
using Domain.Companies;
using Domain.Drivers;
using Domain.Employees;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class TransportDbContext : IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriversLicence> DriversLicences { get; set; }
        public TransportDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
