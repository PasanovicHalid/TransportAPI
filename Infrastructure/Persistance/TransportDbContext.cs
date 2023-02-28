using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class TransportDbContext : IdentityDbContext
    {
        public TransportDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
