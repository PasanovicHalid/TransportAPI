using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;
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
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        if (src.Address.GpsCoordinate is null)
                        {
                            return new Address(src.Address.Street,
                                               src.Address.City,
                                               src.Address.State,
                                               src.Address.PostalCode,
                                               src.Address.Country,
                                               null);
                        }
                        return new Address(src.Address.Street,
                                               src.Address.City,
                                               src.Address.State,
                                               src.Address.PostalCode,
                                               src.Address.Country,
                                               new GpsCoordinate
                                               {
                                                   Longitude = src.Address.GpsCoordinate.Longitude,
                                                   Latitude = src.Address.GpsCoordinate.Latitude
                                               });
                    });

                })
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return src.User is not null ? src.User.PhoneNumber : null; 
                    });

                });
        }
    }
    public class EmployeeResponse
    {
        public ulong Id { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public double Salary { get; set; }
        public Address Address { get; set; }
        public ulong CompanyId { get; set; }
    }
}
