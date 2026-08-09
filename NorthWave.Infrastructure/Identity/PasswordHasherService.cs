using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NorthWave.Application.Interfaces.ServicesInterfaces;

namespace NorthWave.Infrastructure.Identity
{


    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _hasher.VerifyHashedPassword(
                null!,
                hashedPassword,
                password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
