using NorthWave.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order?> GetByIdAsync(int id);

        Task<Order?> GetByIdWithItemsAsync(int id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Order order);
    }
}
