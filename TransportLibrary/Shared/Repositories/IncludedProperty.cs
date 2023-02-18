using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Shared.Repositories
{
    public class IncludedProperty
    {
        public IncludedProperty(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
