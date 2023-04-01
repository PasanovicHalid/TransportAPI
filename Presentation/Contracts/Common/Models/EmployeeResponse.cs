using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Common.Models
{
    public class EmployeeResponseAdapter : Profile
    {
        public EmployeeResponseAdapter()
        {
            CreateMap<Employee, EmployeeResponse>();
        }
    }
    public class EmployeeResponse
    {
        public ulong Id { get; set; }
        public string Role { get; private set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
        public ulong CompanyId { get; private set; }
    }
}
