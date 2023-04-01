using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Contracts.Vans
{
    public class CreateVanRequest
    {
        public double Width { get; set; }
        public double Depth { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
        public double WidthCompartment { get; set; }
        public double DepthCompartment { get; set; }
        public double HeightCompartment { get; set; }
        public double MaxCarryWeight { get; set; }
    }
}
