using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.DTOs.Order
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }

        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
