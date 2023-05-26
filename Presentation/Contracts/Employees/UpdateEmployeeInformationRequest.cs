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
            CreateMap<UpdateEmployeeInformationRequest, UpdateEmployeeInformationByIdCommand>()
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return MapAddress(src);
                    });
                });

            CreateMap<UpdateEmployeeInformationRequest, UpdateEmployeeInformationByIdentityCommand>()
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        return MapAddress(src);
                    });
                });
        }

        private static Domain.ValueObjects.Address? MapAddress(UpdateEmployeeInformationRequest src)
        {
            if (src.Address != null)
            {
                Domain.ValueObjects.Address address = new Domain.ValueObjects.Address(src.Address.Street,
                                                       src.Address.City,
                                                       src.Address.State,
                                                       src.Address.PostalCode,
                                                       src.Address.Country);
                if (src.Address.GpsCoordinate != null)
                {
                    if (src.Address.GpsCoordinate.Longitude != null && src.Address.GpsCoordinate.Latitude != null)
                        address.GpsCoordinate = new Domain.ValueObjects.GpsCoordinate(src.Address.GpsCoordinate!.Longitude!.Value,
                                                                                  src.Address.GpsCoordinate!.Latitude!.Value);
                    return address;
                }
                else
                {
                    return address;
                }
            }
            else
            {
                return null;
            }
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
