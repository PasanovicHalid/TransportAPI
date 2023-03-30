using Application.Drivers.Commands.DriverLicenses.Create;
using Application.Drivers.Commands.DriverLicenses.Delete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Licences.DriversLicences
{
    public class DeleteDriversLicenceAdapter : Profile
    {
        public DeleteDriversLicenceAdapter()
        {
            CreateMap<DeleteDriversLicenceRequest, DeleteDriversLicenseCommand>();
        }
    }
    public class DeleteDriversLicenceRequest
    {
        public ulong Id { get; set; }

        public ulong DriverId { get; set; }
    }
}
