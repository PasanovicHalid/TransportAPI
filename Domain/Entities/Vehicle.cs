using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Vehicle : EntityObject
    {
        public string Manufacturer { get; private set; }

        public string Model { get; private set; }

        public DateTime DateOfManufacturing { get; private set; }

        public Dimensions Dimensions { get; private set; }

        public List<Trailer> Trailers { get; private set; } = new();

        public Company OwnedBy { get; private set; }

        [ForeignKey(nameof(OwnedBy))]
        
        public ulong CompanyId { get; private set; }

        public Vehicle(string manufacturer, string model, DateTime dateOfManufacturing, Dimensions dimensions)
        {
            Manufacturer = manufacturer;
            Model = model;
            DateOfManufacturing = dateOfManufacturing;
            Dimensions = dimensions;
        }

        protected Vehicle() { }
    }
}
