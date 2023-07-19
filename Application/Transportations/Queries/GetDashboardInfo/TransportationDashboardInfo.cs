using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transportations.Queries.GetDashboardInfo
{
    public class TransportationDashboardInfo
    {
        public int TotalPendingTransportations { get; set; }
        public int TotalCompletedTransportations { get; set; }
        public double TotalKilometersDriven { get; set; }
    }
}
