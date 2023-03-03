using Application.Common.Interfaces.Persistance;
using Application.Common.Interfaces.Persistance.Employees;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransportDbContext _context;
        private IUserRepository? _userRepository;

        public UnitOfWork(TransportDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
