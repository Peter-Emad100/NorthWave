using NorthWave.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(int id);

        Task<OrderDto> CreateAsync(CreateOrderDto dto);

        Task UpdateStatusAsync(int id, UpdateOrderStatusDto dto);

        Task DeleteAsync(int id);
    }
}
