using Application.DriverLicenses.Commands.Create;
using Application.Trailers.Commands.Create;
using AutoMapper;
using Presentation.Contracts.Licences.DriversLicences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Trailers
{
    public class UpdateTrailerRequest
    {
        public double Width { get; set; }

        public double Depth { get; set; }

        public double Height { get; set; }

        public double MaxCarryWeight { get; set; }
    }
}
