using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Shared.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public void Save();

    }
}
