using Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Licences.DriverLicences.Queries.FindByIdByAdmin
{
    public class FindDriversLicenseByIdByAdminCommand : IRequest<Result<DriversLicense>>
    {
        public ulong Id { get; set; }

        public string AdminIdentityId { get; set; }
    }
}
