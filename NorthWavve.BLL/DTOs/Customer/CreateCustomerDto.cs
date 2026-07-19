using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWave.Models.Entities;
namespace NorthWave.BLL.DTOs.Customer
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = string.Empty;

        public CustomerType CustomerType { get; set; }
    }
}
