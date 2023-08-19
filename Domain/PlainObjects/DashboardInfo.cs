using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PlainObjects
{
    public class DashboardInfo
    {
        public int VehiclesCount { get; set; }
        public double Inflow { get; set; }
        public double Outflow { get; set; }
        public int TransportationsInProgress { get; set; }
        public double EmployeeExpenses { get; set; }
        public List<ChartDatapoint> TransportationGainsPerDay { get; set; } = new();
        public List<ChartDatapoint> TransportationCostsPerDay { get; set; } = new();
        public List<ChartDatapoint> TransportationCountPerDay { get; set; } = new();
    }
}
