using Application.Common.Interfaces.Persistence.Transportations;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Transportations
{
    public class TransportationRepository : EntityRepository<Transportation>, ITransportationRepository
    {
        public TransportationRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
