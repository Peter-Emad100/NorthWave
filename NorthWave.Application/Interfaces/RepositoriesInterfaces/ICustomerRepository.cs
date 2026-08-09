
using NorthWave.Domain.Entities;

namespace NorthWave.Application.Interfaces.RepositoriesInterfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> GetByEmailAsync(string email);

        Task AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(Customer customer);

        Task<bool> ExistsAsync(int id);
    }
}
