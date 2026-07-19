using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWave.Models.Enums;
namespace NorthWave.BLL.DTOs.Customer
{
    public class CreateCustomerDto
    {
        public required string Name { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}
