using NorthWave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Domain.Discounts
{
    public class EmployeeDiscountStrategy : IDiscountStrategy
    {
        public CustomerType CustomerType => CustomerType.Employee;

        public decimal ApplyDiscount(decimal total)
        {
            return total * 0.5m;
        }
    }
}
