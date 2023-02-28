using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Employees.Model;

namespace TransportLibrary.Drivers.Model
{
    public class Driver : Employee
    {
        public List<DriversLicence> DriversLicences { get; private set; }

        public Driver(Salary salary, List<DriversLicence> driversLicences) : base(salary)
        {
            DriversLicences = driversLicences;
        }
    }
}
