using Application.Common.Interfaces.Persistence.Companies;
using Domain.Entities;
using Infrastructure.Common.Persistence;

namespace Infrastructure.Persistence.Repositories.Companies
{
    public class CompanyRepository : EntityRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
