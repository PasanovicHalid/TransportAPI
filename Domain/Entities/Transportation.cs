using Domain.Common;
using Domain.ValueObjects;
using FluentResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transportation : EntityObject
    {
        public Transportation(DateTime start, DateTime requiredFor, Cargo transporting, Address destination, Address origin, Money received, ulong companyId)
        {
            Start = start;
            RequiredFor = requiredFor;
            Transporting = transporting;
            Destination = destination;
            Received = received;
            CompanyId = companyId;
            Origin = origin;
        }

        protected Transportation() { }

        public DateTime Start { get; private set; }

        public DateTime RequiredFor { get; private set; }

        public Cargo Transporting { get; private set; }

        public Address Destination { get; private set; }

        public Address Origin { get; private set; }

        public Money? Cost { get; private set; }

        public Money Received { get; private set; }

        public GpsCoordinate? StartLocation { get; private set; }

        [ForeignKey(nameof(DriverId))]
        public Driver? DrivenBy { get; private set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? DesignatedTo { get; private set; }

        public ulong? DriverId { get; private set; }
        public ulong CompanyId { get; private set; }

        public Result AddResolution(Money cost, ulong drivenBy, GpsCoordinate startLocation)
        {
            Cost = cost;
            DriverId = drivenBy;
            StartLocation = startLocation;
            return Result.Ok();
        }

        public Result UpdateInformation(DateTime start, DateTime requiredFor, Cargo transporting, Address destination, Address origin, Money received)
        {
            Start = start;
            RequiredFor = requiredFor;
            Transporting = transporting;
            Destination = destination;
            Origin = origin;
            Received = received;
            return Result.Ok();
        }

        public Result<double> GetDistanceToDestination()
        {
            if (StartLocation is null)
                return Result.Fail<double>("Transportation has no start location");

            if (Destination.GpsCoordinate is null)
                return Result.Fail<double>("Transportation has no destination gps coordinate");

            if (Origin.GpsCoordinate is null)
                return Result.Fail<double>("Transportation has no origin gps coordinate");

            return StartLocation.DistanceTo(Origin.GpsCoordinate) + Origin.GpsCoordinate.DistanceTo(Destination.GpsCoordinate) ;
        }
    }
}
