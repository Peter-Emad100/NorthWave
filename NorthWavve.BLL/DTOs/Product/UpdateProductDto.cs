using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.DTOs.Product
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }

        public decimal Price { get; set; }
    }
}
