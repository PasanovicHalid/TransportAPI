﻿using Domain.Common;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Employee : EntityObject
    {
        [ForeignKey(nameof(IdentityId))]
        public IdentityUser? User { get; private set; }

        public string Role { get; private set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public double Salary { get; set; }

        public Address Address { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; private set; }

        public ulong CompanyId { get; private set; }
        public string IdentityId { get; private set; }

        public Employee(string identityId,
                        string role,
                        string firstName,
                        string? middleName,
                        string lastName,
                        double salary,
                        Address address,
                        ulong companyId)
        {
            IdentityId = identityId;
            Role = role;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Salary = salary;
            Address = address;
            CompanyId = companyId;
        }

        public Employee(IdentityUser user,
                        string role,
                        string firstName,
                        string? middleName,
                        string lastName,
                        double salary,
                        Address address,
                        Company company)
        {
            User = user;
            IdentityId = user.Id;
            Role = role;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Salary = salary;
            Address = address;
            Company = company;
            CompanyId = company.Id;
        }

        public Employee(IdentityUser user,
                        string role,
                        string firstName,
                        string? middleName,
                        string lastName,
                        double salary,
                        Address address,
                        ulong companyId)
        {
            User = user;
            IdentityId = user.Id;
            Role = role;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Salary = salary;
            Address = address;
            CompanyId = companyId;
        }

        public Employee(string identityId,
                        string role,
                        string firstName,
                        string? middleName,
                        string lastName,
                        double salary,
                        Address address,
                        Company company)
        {
            IdentityId = identityId;
            Role = role;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Salary = salary;
            Address = address;
            Company = company;
            CompanyId = company.Id;
        }

        protected Employee()
        {
        }
    }
}
