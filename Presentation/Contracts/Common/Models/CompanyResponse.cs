using AutoMapper;
using Domain.Entities;
using Presentation.Contracts.Common.ValueObjects;
using System.Collections.Generic;

namespace Presentation.Contracts.Common.Models
{
    public class CompanyResponseAdapter : Profile
    {
        public CompanyResponseAdapter()
        {
            CreateMap<Company, CompanyResponse>();
        }

        private static TrailerResponse MapTrailer(Trailer trailer)
        {
            return new()
            {
                Capacity = new()
                {
                    MaxCarryWeight = trailer.Capacity.MaxCarryWeight,
                    Volume = new()
                    {
                        Depth = trailer.Capacity.Volume.Depth,
                        Height = trailer.Capacity.Volume.Height,
                        Width = trailer.Capacity.Volume.Width,
                    }
                },
                CompanyId = trailer.CompanyId,
                Id = trailer.Id,
                VehicleId = trailer.VehicleId,
            };
        }

        private static EmployeeResponse MapEmployee(Employee employee)
        {
            EmployeeResponse mappedEmployee = new()
            {
                Address = MapAddress(employee.Address),
                CompanyId = employee.CompanyId,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Role = employee.Role,
                MiddleName = employee.MiddleName,
                Salary = employee.Salary,
            };

            if (employee.User is not null)
            {
                mappedEmployee.PhoneNumber = employee.User.PhoneNumber;
            }

            return mappedEmployee;
        }

        private static VehicleResponse MapVehicle(Vehicle vehicle)
        {
            return new()
            {
                CompanyId = vehicle.CompanyId,
                Id = vehicle.Id,
                Model = vehicle.Model,
                DateOfManufacturing = vehicle.DateOfManufacturing,
                Dimensions = new()
                {
                    Width = vehicle.Dimensions.Width,
                    Depth = vehicle.Dimensions.Depth,
                },
                DriverId = vehicle.DriverId,
                Manufacturer = vehicle.Manufacturer,
            };
        }

        private static Address MapAddress(Domain.ValueObjects.Address address)
        {
            if (address.GpsCoordinate is null)
            {
                return new Address(address.Street,
                                   address.City,
                                   address.State,
                                   address.PostalCode,
                                   address.Country,
                                   null);
            }
            return new Address(address.Street,
                               address.City,
                               address.State,
                               address.PostalCode,
                               address.Country,
                               new GpsCoordinate
                               {
                                   Latitude = address.GpsCoordinate.Latitude,
                                   Longitude = address.GpsCoordinate.Longitude
                               });
        }
    }
    public class CompanyResponse
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<VehicleResponse> Vehicles { get; set; }
        public List<EmployeeResponse> Employees { get; set; }
        public List<TrailerResponse> Trailers { get; set; }
    }
}
