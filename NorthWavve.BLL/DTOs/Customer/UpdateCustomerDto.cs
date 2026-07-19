using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.DTOs.Customer
{
    public class UpdateCustomerDto
    {

        public required string Name { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}
