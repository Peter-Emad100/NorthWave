using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Discounts
{
    public class VipDiscountStrategy : IDiscountStrategy
    {
        public CustomerType CustomerType => CustomerType.VIP;

        public decimal ApplyDiscount(decimal total)
        {
            return total * 0.8m;
        }
    }
}
