using Microsoft.AspNetCore.Identity;
using NorthWave.BLL.DTOs.Auth;
using NorthWave.BLL.DTOs.Customer;
using NorthWave.BLL.Interfaces;
using NorthWave.DAL.Interfaces;
using NorthWave.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<Customer> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(ICustomerRepository customerRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _jwtService = jwtService;
            _passwordHasher = new PasswordHasher<Customer>();
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var customer = await _customerRepository.GetByEmailAsync(loginDto.Email);

            if (customer == null)
                throw new Exception("Invalid email or password.");

            var result = _passwordHasher.VerifyHashedPassword(
                null!,
                customer.Password,
                loginDto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password.");

            return new AuthResponseDto
            {
                Token = _jwtService.GenerateToken(customer),

                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    CreatedAt = customer.CreatedAt
                }
            };
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(dto.Email);
            if (existingCustomer != null)
            {
                throw new Exception("User with this email already exists.");
            }
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = _passwordHasher.HashPassword(null!, dto.Password),
                CreatedAt = DateTime.UtcNow
            };
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                Customer = new CustomerDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    CreatedAt = customer.CreatedAt
                },

                Token = _jwtService.GenerateToken(customer)
            };
        }
    }
}
