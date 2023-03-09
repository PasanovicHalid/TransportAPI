using Application.Common.Interfaces.Persistance.Companies;
using Domain.Companies;
using Infrastructure.Common.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories.Companies
{
    public class CompanyRepository : EntityRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(TransportDbContext db) : base(db)
        {
        }
    }
}
