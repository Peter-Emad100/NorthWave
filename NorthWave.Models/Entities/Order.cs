using NorthWave.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public OrderStatus Status { get; set; } = OrderStatus.New;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
