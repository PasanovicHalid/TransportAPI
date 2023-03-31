using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
