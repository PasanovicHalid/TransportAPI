using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Cost : EntityObject
    {
        public Cost(Stop from, Stop to, Money expendature)
        {
            From = from;
            To = to;
            FromId = from.Id;
            ToId = to.Id;
            Expendature = expendature;
        }
        protected Cost() { }

        public Stop? From { get; private set; }
        public Stop? To { get; private set; }
        public ulong? FromId { get; private set; }
        public ulong? ToId { get; private set; }
        public Money Expendature { get; private set; }
    }
}
