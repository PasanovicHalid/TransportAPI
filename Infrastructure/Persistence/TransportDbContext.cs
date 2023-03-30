﻿using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TransportDbContext : IdentityDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Van> Vans { get; set; }

        public TransportDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .HasDiscriminator(e => e.Role)
                .IsComplete(false)
                .HasValue<Driver>(ApplicationRolesConstants.Driver);

            base.OnModelCreating(builder);
        }
    }
}