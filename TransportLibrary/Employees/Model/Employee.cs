using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication.Model;

namespace TransportLibrary.Employees.Model
{
    public class Employee
    {
        public Guid UserId { get; set; } 
        public Salary Salary { get; private set; }

        public Employee(Salary salary)
        {
            Salary = salary;
        }
    }
}
