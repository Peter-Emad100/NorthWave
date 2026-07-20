using Microsoft.Extensions.Logging;
using NorthWave.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthWave.BLL.DTOs.Order;
namespace NorthWave.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendOrderConfirmationAsync(OrderDto dto)
        {
            _logger.LogInformation(
                "[EMAIL] To: {CustomerName} - Your order #{OrderId} totalling {Total:C} was received.",
                dto.CustomerName,
                dto.Id,
                dto.Total);
            return Task.CompletedTask;
        }
    }
}
