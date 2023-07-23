using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

namespace Presentation.Contracts.Common.Models
{
    public class EmployeeResponseAdapter : Profile
    {
        public EmployeeResponseAdapter()
        {
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return src.User is not null ? src.User.PhoneNumber : null;
                    });
                })
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return src.User is not null ? src.User.Email : null;
                    });
                })
                .ForMember(dest => dest.VehicleId, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return src is Driver driver ? driver.VehicleId : null;
                    });
                });
        }
    }
    public class EmployeeResponse
    {
        public ulong Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
        public ulong CompanyId { get; set; }
        public ulong VehicleId { get; set; }
    }
}
