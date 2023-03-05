using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Companies
{
    public class UpdateCompanyRequest
    {
        public ulong Id { get; set; }

        public string Name { get; set; }
    }
}
