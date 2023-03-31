using Application.Common.Interfaces.Persistence.Transportations;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Transportations
{
    public class StopRepository : EntityRepository<Stop>, IStopRepository
    {
        public StopRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
