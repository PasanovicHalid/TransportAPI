using Domain.Common;

namespace Domain.ValueObjects
{
    public class Volume : ValueObject
    {
        public double Width { get; private set; }
        public double Depth { get; private set; }
        public double Height { get; private set; }

        public Volume(double width, double depth, double height)
        {
            Width = width;
            Depth = depth;
            Height = height;
        }

        protected Volume() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Width;
            yield return Depth;
            yield return Height;
        }
    }
}
