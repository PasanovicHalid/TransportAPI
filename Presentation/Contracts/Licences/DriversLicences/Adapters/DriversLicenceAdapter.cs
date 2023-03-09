using Application.Licences.DriverLicences.Commands.Create;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Licences.DriversLicences.Adapters
{
    public class DriversLicenceAdapter : Profile
    {
        public DriversLicenceAdapter()
        {
            CreateMap<CreateDriversLicenceRequest, CreateDriversLicenceCommand>();
        }
    }
}
