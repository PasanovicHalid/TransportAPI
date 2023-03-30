using Application.Licences.DriverLicences.Commands.Update;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public ulong Id { get; set; }
        public string Category { get; set; }
        public DateTime IssuingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
