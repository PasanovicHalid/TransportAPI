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
    public class Trailer : EntityObject
    {
        public Capacity Capacity { get; private set; }

        [ForeignKey(nameof(CompanyId))]
        public Company OwnedBy { get; private set; }

        [ForeignKey(nameof(VehicleId))]
        public Vehicle? UsedBy { get; private set; }

        public ulong? CompanyId { get; private set; }
        public ulong? VehicleId { get; private set; }

        protected Trailer() { }
    }
}
