namespace Presentation.Contracts.Trailers
{
    public class CreateTrailerRequest
    {
        public double Width { get; set; }

        public double Depth { get; set; }

        public double Height { get; set; }

        public double MaxCarryWeight { get; set; }
    }
}
