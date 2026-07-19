using NorthWave.BLL.Discounts;
using NorthWave.BLL.DTOs.Order;
using NorthWave.BLL.Interfaces;
using NorthWave.DAL.Interfaces;
using NorthWave.Models.Entities;
using NorthWave.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        public OrderService(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _discountStrategies = discountStrategies;
        }
        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            return orders.Select(MapOrderToDto);
        }
        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(id);

            if (order == null)
                return null;

            return MapOrderToDto(order);
        }
        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            var order = new Order
            {
                CustomerId = customer.Id,
                Customer = customer,
                Status = OrderStatus.New
            };

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product == null)
                    throw new KeyNotFoundException(
                        $"Product {item.ProductId} not found.");

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            await _orderRepository.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return MapOrderToDto(order);
        }
        public async Task<OrderDto> UpdateStatusAsync(int id,UpdateOrderStatusDto dto)
        {
            var order = await _orderRepository.GetByIdWithItemsAsync(id);

            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            order.Status = dto.Status;

            await _orderRepository.UpdateAsync(order);

            await _unitOfWork.SaveChangesAsync();

            return MapOrderToDto(order);
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException("Order not found.");

            await _orderRepository.DeleteAsync(order);

            await _unitOfWork.SaveChangesAsync();
        }
        private OrderDto MapOrderToDto(Order order)
        {
            decimal subTotal = order.OrderItems.Sum(i => i.UnitPrice * i.Quantity);

            var strategy = _discountStrategies.First(
                s => s.CustomerType == order.Customer.CustomerType);

            decimal total = strategy.ApplyDiscount(subTotal);

            return new OrderDto
            {
                Id = order.Id,
                CustomerName = order.Customer.Name,
                CustomerType = order.Customer.CustomerType,
                Status = order.Status,
                Total = total,

                OrderItems = order.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    SubTotal = i.UnitPrice * i.Quantity
                }).ToList()
            };
        }
    }
}
