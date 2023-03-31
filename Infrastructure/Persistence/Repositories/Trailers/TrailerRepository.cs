using Application.Common.Interfaces.Persistence.Trailers;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Trailers
{
    public class TrailerRepository : EntityRepository<Trailer>, ITrailerRepository
    {
        public TrailerRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
