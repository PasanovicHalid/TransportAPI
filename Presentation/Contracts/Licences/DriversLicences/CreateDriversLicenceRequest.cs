using Application.DriverLicenses.Commands.Create;
using AutoMapper;

namespace Presentation.Contracts.Licences.DriversLicences
{
    public class CreateDriversLicenceAdapter : Profile
    {
        public CreateDriversLicenceAdapter()
        {
            CreateMap<CreateDriversLicenceRequest, CreateDriversLicenseCommand>();
        }
    }
    public class CreateDriversLicenceRequest
    {
        public string Category { get; set; }

        public DateTime IssuingDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
