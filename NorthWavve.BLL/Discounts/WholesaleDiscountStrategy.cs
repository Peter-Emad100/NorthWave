using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Discounts
{
    public class WholesaleDiscountStrategy : IDiscountStrategy
    {
        public CustomerType CustomerType => CustomerType.Wholesale;

        public decimal ApplyDiscount(decimal total)
        {
            return total * 0.85m;
        }
    }
}
