using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Settings;
using TransportLibrary.Shared.Repositories.Interfaces;

namespace TransportLibrary.Shared.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportDbContext _context;

        public UnitOfWork(TransportDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
