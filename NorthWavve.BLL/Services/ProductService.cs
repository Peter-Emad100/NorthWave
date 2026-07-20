using Microsoft.Extensions.Logging;
using NorthWave.BLL.DTOs.Product;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            await _productRepository.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product created with ID: {ProductId}", product.Id);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            product.Name = dto.Name;
            product.Price = dto.Price;

            await _productRepository.UpdateAsync(product);

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product updated with ID: {ProductId}", product.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

           await _productRepository.DeleteAsync(product);

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("Product deleted with ID: {ProductId}", product.Id);
        }
    }
}
