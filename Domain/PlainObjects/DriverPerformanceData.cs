using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PlainObjects
{
    public class DriverPerformanceData
    {
        public List<ChartDatapoint> NumberOfTransportations { get; set; } = new();
    }
}
