using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;

        public  required decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
