using Domain.Common;

namespace Domain.ValueObjects
{
    public class Capacity : ValueObject
    {
        public Volume Volume { get; private set; }
        public double MaxCarryWeight { get; private set; }

        public Capacity(Volume volume, double maxCarryWeight)
        {
            Volume = volume;
            MaxCarryWeight = maxCarryWeight;
        }

        public Capacity(double width, double depth, double height, double maxCarryWeight)
        {
            Volume = new Volume(width, depth, height);
            MaxCarryWeight = maxCarryWeight;
        }

        protected Capacity() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Volume;
            yield return MaxCarryWeight;
        }
    }
}
