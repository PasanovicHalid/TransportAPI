using Application.Common.Interfaces.Persistence.Employees;
using Domain.Entities;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Employees
{
    public class AdministratorRepository : EntityRepository<Admininistrator>, IAdministratorRepository
    {
        public AdministratorRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
