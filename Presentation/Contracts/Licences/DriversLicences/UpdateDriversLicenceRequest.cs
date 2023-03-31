using Application.DriverLicenses.Commands.Update;
using AutoMapper;

namespace Presentation.Contracts.Licences.DriversLicences
{
    public class UpdateDriversLicenceAdapter : Profile
    {
        public UpdateDriversLicenceAdapter()
        {
            CreateMap<UpdateDriversLicenceRequest, UpdateDriversLicenseCommand>();
        }
    }
    public class UpdateDriversLicenceRequest
    {
        public string Category { get; set; }
        public DateTime IssuingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
