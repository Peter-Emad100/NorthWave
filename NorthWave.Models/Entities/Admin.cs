using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Models.Entities
{
    public class Admin
    {
            public int Id { get; set; }
            public required string Name { get; set; } = string.Empty;
            public required string Password { get; set; }
            public required string Email { get; set; }
            public string? TwoFactorCode { get; set; }

            public UserRole Role { get; set; } = UserRole.Admin;

            public DateTime? TwoFactorExpiry { get; set; }
            public DateTime CreatedAt { get; set; }

        }
    }
