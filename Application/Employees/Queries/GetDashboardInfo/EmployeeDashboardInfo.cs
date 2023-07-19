using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Queries.GetDashboardInfo
{
    public class EmployeeDashboardInfo
    {
        public int TotalEmployees { get; set; }
        public double TotalEmployeeExpenses { get; set; }
        public int TotalAdmins { get; set; }
        public double TotalAdminExpenses { get; set; }
        public int TotalDrivers { get; set; }
        public double TotalDriverExpenses { get; set; }
    }
}
