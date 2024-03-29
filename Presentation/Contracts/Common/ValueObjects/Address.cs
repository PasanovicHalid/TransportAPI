﻿using AutoMapper;

namespace Presentation.Contracts.Common.ValueObjects
{
    public class AddressAdapter : Profile
    {
        public AddressAdapter() 
        {
            CreateMap<Address, Domain.ValueObjects.Address>().ForMember(dest => dest.GpsCoordinate, opt =>
            {
                opt.Condition(src => src.GpsCoordinate != null && (src.GpsCoordinate.Latitude != null || src.GpsCoordinate.Longitude != null));
            });
            CreateMap<Address, Domain.ValueObjects.Address>().ReverseMap();
        }
    }
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public GpsCoordinate? GpsCoordinate { get; set; }

        public Address(string street, string city, string state, string postalCode, string country, GpsCoordinate? gpsCoordinate = null)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
            GpsCoordinate = gpsCoordinate;
        }

        public Address()
        {
        }
    }
}
