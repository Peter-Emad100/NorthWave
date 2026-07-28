using NorthWave.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer);
    }
}
