using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public required string Name { get; set; } = string.Empty;

        public CustomerType CustomerType { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
