using Domain.Common;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Trailer : EntityObject
    {
        public Capacity Capacity { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public Company OwnedBy { get; private set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicle? UsedBy { get; private set; }

        public ulong CompanyId { get; private set; }
        public ulong? VehicleId { get; set; }

        protected Trailer() { }

        public Trailer(Capacity capacity, ulong companyId)
        {
            Capacity = capacity;
            CompanyId = companyId;
        }
    }
}
