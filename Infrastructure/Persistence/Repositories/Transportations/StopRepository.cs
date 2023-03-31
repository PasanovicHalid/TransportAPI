using Application.Common.Interfaces.Persistence.Transportations;
using Domain.Entities;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Transportations
{
    public class StopRepository : EntityRepository<Stop>, IStopRepository
    {
        public StopRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
