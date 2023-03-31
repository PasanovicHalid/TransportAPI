using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Van : Vehicle
    {
        public Capacity Capacity { get; set; }

        public Van(Capacity capacity)
        {
            Capacity = capacity;
        }

        protected Van() { }

        public Van(string manufacturer,
                   string model,
                   DateTime dateOfManufacturing,
                   Dimensions dimensions,
                   Capacity capacity) : base(manufacturer,
                                                 model,
                                                 dateOfManufacturing,
                                                 dimensions)
        {
            Capacity = capacity;
        }
    }
}
