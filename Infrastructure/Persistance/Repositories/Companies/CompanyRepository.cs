using Application.Common.Interfaces.Persistance.Companies;
using Domain.Companies;
using Infrastructure.Common.Persistance;

namespace Infrastructure.Persistance.Repositories.Companies
{
    public class CompanyRepository : EntityRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
