namespace Presentation.Contracts.Trucks
{
    public class CreateTruckRequest
    {
        public double Width { get; set; }
        public double Depth { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime DateOfManufacturing { get; set; }
    }
}
