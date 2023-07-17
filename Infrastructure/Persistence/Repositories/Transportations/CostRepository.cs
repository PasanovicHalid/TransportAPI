using Application.Common.Interfaces.Persistence.Transportations;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Transportations
{
    public class CostRepository : EntityRepository<Cost>, ICostRepository
    {
        public CostRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
