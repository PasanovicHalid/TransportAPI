using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;

namespace Presentation.Contracts.Common.Models
{
    public class VanResponseAdapter : Profile
    {
        public VanResponseAdapter()
        {
            CreateMap<Van, VanResponse>()
                .ForMember(dest => dest.Dimensions, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return new Dimensions
                        {
                            Width = src.Dimensions.Width,
                            Depth = src.Dimensions.Depth
                        };
                    });
                })
                .ForMember(dest => dest.Driver, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        if (src.Driver is null)
                        {
                            return null;
                        }
                        return new EmployeeResponse
                        {
                            Id = src.Driver.Id,
                            FirstName = src.Driver.FirstName,
                            MiddleName = src.Driver.MiddleName,
                            LastName = src.Driver.LastName,
                            CompanyId = src.Driver.CompanyId,
                            Role = src.Driver.Role,
                            PhoneNumber = src.Driver.User is not null ? src.Driver.User.PhoneNumber : null,
                            Salary = src.Driver.Salary,
                            Address = new Address
                            {
                                Street = src.Driver.Address.Street,
                                City = src.Driver.Address.City,
                                State = src.Driver.Address.State,
                                PostalCode = src.Driver.Address.PostalCode,
                                Country = src.Driver.Address.Country,
                                GpsCoordinate = src.Driver.Address.GpsCoordinate is not null ? new GpsCoordinate
                                {
                                    Longitude = src.Driver.Address.GpsCoordinate.Longitude,
                                    Latitude = src.Driver.Address.GpsCoordinate.Latitude
                                } : null
                            }
                        };
                    });
                })
                .ForMember(dest => dest.Capacity, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return new Capacity
                        {
                            MaxCarryWeight = src.Capacity.MaxCarryWeight,
                            Volume = new Volume
                            {
                                Depth = src.Capacity.Volume.Depth,
                                Height = src.Capacity.Volume.Height,
                                Width = src.Capacity.Volume.Width
                            }
                        };
                    });
                });
        }
    }

    public class VanResponse
    {
        public ulong Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public Dimensions Dimensions { get; set; }
        public Capacity Capacity { get; set; }
        public ulong CompanyId { get; set; }
        public ulong? DriverId { get; set; }
        public EmployeeResponse? Driver { get; set; }
    }
}
