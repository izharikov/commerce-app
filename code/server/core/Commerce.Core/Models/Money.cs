using System;

namespace Commerce.Core.Models
{
    public class Money
    {
        public Money(decimal amount, string currency = "USD")
        {
            Amount = Math.Abs(amount);
            Currency = currency;
        }

        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}