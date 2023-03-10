using Application.Licences.DriverLicences.Commands.Create;
using AutoMapper;

namespace Presentation.Contracts.Licences.DriversLicences
{
    public class DriversLicenceAdapter : Profile
    {
        public DriversLicenceAdapter()
        {
            CreateMap<CreateDriversLicenceRequest, CreateDriversLicenceCommand>();
        }
    }
    public class CreateDriversLicenceRequest
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ulong DriverId { get; set; }
    }
}
