using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Company : EntityObject
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public IEnumerable<Employee> Employees { get; private set; } = new List<Employee>();

        public IEnumerable<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public IEnumerable<Trailer> Trailers { get; private set; } = new List<Trailer>();

        public Company(string name,
                       IEnumerable<Employee> employees,
                       IEnumerable<Vehicle> vehicles,
                       IEnumerable<Trailer> trailers,
                       Address address)
        {
            Name = name;
            Employees = employees;
            Vehicles = vehicles;
            Trailers = trailers;
            Address = address;
        }

        protected Company()
        {
        }

        public Company(string name, Address address)
        {
            Name = name;
            Address = address;
        }
    }
}