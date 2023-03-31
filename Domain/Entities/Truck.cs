using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Truck : Vehicle
    {
        public Truck(string manufacturer,
                     string model,
                     DateTime dateOfManufacturing,
                     Dimensions dimensions) : base(manufacturer,
                                                   model,
                                                   dateOfManufacturing,
                                                   dimensions)
        {
        }

        protected Truck() { }
    }
}
