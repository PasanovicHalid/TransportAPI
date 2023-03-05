using Application.Common.Interfaces.Persistance.Employees;
using Domain;
using Infrastructure.Common.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Employees
{
    public class DriverRepository : EntityRepository<Driver>, IDriverRepository
    {
        public DriverRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
