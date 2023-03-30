using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Company : EntityObject
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public List<Employee> Employees { get; private set; } = new();

        public List<Vehicle> Vehicles { get; private set; } = new();

        public List<Trailer> Trailers { get; private set; } = new();

        public Company(string name,
                       List<Employee> employees,
                       List<Vehicle> vehicles,
                       List<Trailer> trailers,
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