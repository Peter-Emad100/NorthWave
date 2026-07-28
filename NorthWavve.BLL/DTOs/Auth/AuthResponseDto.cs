using NorthWave.BLL.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.DTOs.Auth
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }

        public required CustomerDto Customer { get; set; }
    }
}
