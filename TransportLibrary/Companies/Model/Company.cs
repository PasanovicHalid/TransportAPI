using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Model;
using TransportLibrary.Employees.Model;
using TransportLibrary.Shared.Model.ValueObjects;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Companies.Model
{
    public class Company : EntityObject
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<Employee> Employees { get; set; }

    }
}
