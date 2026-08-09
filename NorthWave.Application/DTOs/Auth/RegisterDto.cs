using NorthWave.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Application.DTOs.Auth
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

        public CustomerType CustomerType { get; set; }
    }
}
