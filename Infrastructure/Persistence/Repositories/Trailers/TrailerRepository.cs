using Application.Common.Interfaces.Persistence.Trailers;
using Domain.Entities;
using Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Trailers
{
    public class TrailerRepository : EntityRepository<Trailer>, ITrailerRepository
    {
        public TrailerRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
