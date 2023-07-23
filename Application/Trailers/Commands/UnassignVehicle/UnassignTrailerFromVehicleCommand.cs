using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Trailers.Commands.UnassignVehicle
{
    public class UnassignTrailerFromVehicleCommand : IRequest<Result>
    {
        public ulong TrailerId { get; set; }
        public ulong CompanyId { get; set; }
    }
}
