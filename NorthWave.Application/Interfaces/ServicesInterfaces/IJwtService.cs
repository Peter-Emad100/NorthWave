using NorthWave.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Application.Interfaces.ServicesInterfaces
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer);
    }
}
