using NorthWave.BLL.DTOs.Customer;
using NorthWave.BLL.Interfaces;
using NorthWave.DAL.Interfaces;
using NorthWave.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                CustomerType = c.CustomerType
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                CustomerType = customer.CustomerType
            };
        }

        public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                CustomerType = dto.CustomerType
            };

            await _customerRepository.AddAsync(customer);

            await _unitOfWork.SaveChangesAsync();
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                CustomerType = customer.CustomerType
            };
        }

        public async Task UpdateAsync(int id, UpdateCustomerDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            customer.Name = dto.Name;
            customer.CustomerType = dto.CustomerType;

            await _customerRepository.UpdateAsync(customer);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            await _customerRepository.DeleteAsync(customer);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
