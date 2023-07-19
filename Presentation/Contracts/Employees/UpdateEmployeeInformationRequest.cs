using Application.Employees.Commands.UpdateInformationById;
using Application.Employees.Commands.UpdateInformationByIdentity;
using AutoMapper;
using Presentation.Contracts.Common.ValueObjects;

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
        public Address Address { get; set; }
    }


}
