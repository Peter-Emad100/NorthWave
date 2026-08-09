using NorthWave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Application.DTOs.Customer
{
    public class UpdateCustomerDto
    {

        public required string Name { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}
