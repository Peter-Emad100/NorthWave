
using NorthWave.Application.DTOs.Auth;
using NorthWave.Application.DTOs.Customer;
using NorthWave.Application.Interfaces;
using NorthWave.Application.Interfaces.ServicesInterfaces;
using NorthWave.Application.Interfaces.RepositoriesInterfaces;
using NorthWave.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public AuthService(ICustomerRepository customerRepository, IJwtService jwtService, IUnitOfWork unitOfWork, IEmailService emailService , IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var customer = await _customerRepository.GetByEmailAsync(loginDto.Email);

            if (customer == null)
                throw new Exception("Invalid email or password.");

            if (!_passwordHasher.VerifyPassword(customer.Password,loginDto.Password))
                throw new Exception("Invalid email or password.");

            customer.TwoFactorCode = GenerateVerificationCode();
            customer.TwoFactorExpiry = DateTime.UtcNow.AddMinutes(2);

            await _unitOfWork.SaveChangesAsync();
            await _emailService.SendEmailAsync(customer.Email,"NorthWave Verification Code",$"Your verification code is {customer.TwoFactorCode}");
            return new LoginResponseDto
            {
                Message = "Verification code sent to your email."
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
                Password = _passwordHasher.HashPassword(dto.Password),
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
        public async Task<AuthResponseDto> VerifyTwoFactorAsync(VerifyTwoFactorDto dto)
        {
            var customer = await _customerRepository.GetByEmailAsync(dto.Email);

            if (customer == null)
                throw new UnauthorizedAccessException("Invalid verification code.");

            if (customer.TwoFactorCode != dto.Code)
                throw new UnauthorizedAccessException("Invalid verification code.");

            if (customer.TwoFactorExpiry == null ||
                customer.TwoFactorExpiry < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Verification code has expired.");

            
            customer.TwoFactorCode = null;
            customer.TwoFactorExpiry = null;

            await _unitOfWork.SaveChangesAsync();

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
        private static string GenerateVerificationCode()
        {
            return RandomNumberGenerator
                .GetInt32(100000, 1000000)
                .ToString();
        }
    }

}
