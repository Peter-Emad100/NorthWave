using Microsoft.Extensions.DependencyInjection;
using NorthWave.Application.Interfaces.ServicesInterfaces;
using NorthWave.Domain.Discounts;
using NorthWave.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWave.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<IDiscountStrategy, EmployeeDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, RegularDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, VipDiscountStrategy>();
            services.AddScoped<IDiscountStrategy, WholesaleDiscountStrategy>();

            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
