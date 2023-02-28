using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Shared.ModelBase;

namespace TransportLibrary.Shared.Model.ValueObjects
{
    [Owned]
    public class Address : ValueObject
    {

        public string Street { get; private set; }

        public string City { get; private set; }

        public string State { get; private set; }

        public string PostalCode { get; private set; }

        public string Country { get; private set; }

        public GpsCoordinate GpsCoordinate { get; private set; }

        public Address(string street, string city, string state, string postalCode, string country, GpsCoordinate gpsCoordinate)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            GpsCoordinate = gpsCoordinate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return PostalCode;
            yield return Country;
            yield return GpsCoordinate;
            yield return State;
        }
    }
}
