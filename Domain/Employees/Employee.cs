using Domain.Common;
using Domain.Companies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Employees
{
    public class Employee : EntityObject
    {
        public string IdentityId { get; private set; }

        [ForeignKey(nameof(IdentityId))]
        public IdentityUser? User { get; private set; }

        public string Role { get; private set; }

        public string FirstName { get; private set; }

        public string? MiddleName { get; private set; }

        public string LastName { get; private set; }

        public double Salary { get; private set; }

        public Address Address { get; private set; }

        public ulong CompanyId { get; private set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; private set; }

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
