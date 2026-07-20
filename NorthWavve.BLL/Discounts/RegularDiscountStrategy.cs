using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Discounts
{
    public class RegularDiscountStrategy:IDiscountStrategy
    {
        public CustomerType CustomerType => CustomerType.Regular;

        public decimal ApplyDiscount(decimal total)
        {
            return total;
        }
    }
}
