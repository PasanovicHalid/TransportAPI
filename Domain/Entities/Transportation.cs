using Domain.Common;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transportation : EntityObject
    {
        public DateTime Start { get; private set; }

        public DateTime RequiredFor { get; private set; }

        public Cargo Transporting { get; private set; }

        public List<Stop> Stops { get; private set; } = new();

        [ForeignKey(nameof(DriverId))]
        public Driver? DrivenBy { get; private set; }

        [ForeignKey(nameof(CompanyId))]
        public Company? DesignatedTo { get; private set; }

        public ulong? DriverId { get; private set; }
        public ulong CompanyId { get; private set; }
    }
}
