using Domain.Common;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Stop : EntityObject
    {
        public Address Destination { get; private set; }

        [ForeignKey(nameof(TransportationId))]
        public Transportation For { get; private set; }

        public ulong TransportationId { get; private set; }

        protected Stop() { }
    }
}
