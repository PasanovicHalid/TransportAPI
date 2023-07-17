using Domain.Common;
using Domain.ValueObjects;
using FluentResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transportation : EntityObject
    {
        public Transportation(DateTime start, DateTime requiredFor, Cargo transporting, List<Stop> stops, Money received, ulong companyId)
        {
            Start = start;
            RequiredFor = requiredFor;
            Transporting = transporting;
            Stops = stops;
            Received = received;
            CompanyId = companyId;
        }

        public Transportation(DateTime start, DateTime requiredFor, Cargo transporting, List<Stop> stops, List<Cost> costs, Money received, ulong? driverId, ulong companyId)
        {
            Start = start;
            RequiredFor = requiredFor;
            Transporting = transporting;
            Stops = stops;
            Costs = costs;
            Received = received;
            DriverId = driverId;
            CompanyId = companyId;
        }

        protected Transportation() { }

        public DateTime Start { get; private set; }

        public DateTime RequiredFor { get; private set; }

        public Cargo Transporting { get; private set; }

        public List<Stop> Stops { get; private set; } = new();

        public List<Cost>? Costs { get; private set; } = new();

        public Money Received { get; private set; }

        [ForeignKey(nameof(DriverId))]
        public Driver? DrivenBy { get; private set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? DesignatedTo { get; private set; }

        public ulong? DriverId { get; private set; }
        public ulong CompanyId { get; private set; }

        public Result AddResolution(List<Cost> costs, ulong drivenBy)
        {
            Costs = costs;
            DriverId = drivenBy;
            return Result.Ok();
        }
    }
}
