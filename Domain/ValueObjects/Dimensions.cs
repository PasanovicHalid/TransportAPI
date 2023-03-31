using Domain.Common;

namespace Domain.ValueObjects
{
    public class Dimensions : ValueObject
    {
        public double Width { get; private set; }
        public double Depth { get; private set; }

        public Dimensions(double width, double depth)
        {
            Width = width;
            Depth = depth;
        }

        protected Dimensions() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Width;
            yield return Depth;
        }
    }
}
