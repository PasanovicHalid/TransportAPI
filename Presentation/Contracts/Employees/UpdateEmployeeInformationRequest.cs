using Application.Companies.Commands.UpdateInformation;
using Application.Employees.Commands.UpdateInformationById;
using Application.Employees.Commands.UpdateInformationByIdentity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Employees
{
    public class UpdateEmployeeInformationAdapter : Profile
    {
        public UpdateEmployeeInformationAdapter() 
        { 
            CreateMap<UpdateEmployeeInformationRequest, UpdateEmployeeInformationByIdCommand>();
            CreateMap<UpdateEmployeeInformationRequest, UpdateEmployeeInformationByIdentityCommand>();
        }
    }

    public class UpdateEmployeeInformationRequest
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public double? Salary { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }


}
