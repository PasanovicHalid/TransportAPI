using Domain.Common;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public double Amount { get; private set; }
        public string Currency { get; private set; }
        public Money(double amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
