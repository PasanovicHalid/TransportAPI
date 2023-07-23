using Domain.Common;
using Domain.ValueObjects;
using FluentResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Vehicle : EntityObject
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public DateTime DateOfManufacturing { get; set; }

        public Dimensions Dimensions { get; set; }

        public List<Trailer> Trailers { get; private set; } = new();

        [ForeignKey(nameof(CompanyId))]
        public Company OwnedBy { get; private set; }

        public ulong CompanyId { get; private set; }

        public Employee? Driver { get; private set; }

        public ulong? DriverId { get; set; }

        public Vehicle(string manufacturer, string model, DateTime dateOfManufacturing, Dimensions dimensions)
        {
            Manufacturer = manufacturer;
            Model = model;
            DateOfManufacturing = dateOfManufacturing;
            Dimensions = dimensions;
        }

        public void UpdateInformation(string manufacturer, string model, DateTime dateOfManufacturing, Dimensions dimensions)
        {
            Manufacturer = manufacturer;
            Model = model;
            DateOfManufacturing = dateOfManufacturing;
            Dimensions = dimensions;
        }

        public Result AssignDriver(Driver driver)
        {
            Driver = driver;
            DriverId = driver.Id;
            return Result.Ok();
        }

        public Result UnassignDriver()
        {
            Driver = null;
            DriverId = null;
            return Result.Ok();
        }

        public Result AssignTrailer(Trailer trailer)
        {
            Trailers.Add(trailer);
            return Result.Ok();
        }

        protected Vehicle() { }
    }
}
