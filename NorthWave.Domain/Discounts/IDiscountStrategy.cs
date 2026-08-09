using NorthWave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Domain.Discounts
{
    public interface IDiscountStrategy
    {
        CustomerType CustomerType { get; }

        decimal ApplyDiscount(decimal total);
    }
}
