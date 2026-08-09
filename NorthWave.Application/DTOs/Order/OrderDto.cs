using NorthWave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public required string CustomerName { get; set; }

        public CustomerType CustomerType { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new();

        public decimal Total { get; set; }
    }
}
