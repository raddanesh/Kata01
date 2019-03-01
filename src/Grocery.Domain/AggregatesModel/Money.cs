using System;
using System.Collections.Generic;
using Grocery.Domain.SeedWork;

namespace Grocery.Domain.AggregatesModel
{
    public class Money : ValueObject
    {
        /// <summary>
        /// Creates a new <see cref="Money"/> instance.
        /// </summary>
        public Money(decimal value)
        {
            if (value < 0) { throw new ArgumentOutOfRangeException(nameof(value)); }

            Value = value;
        }

        public decimal Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}