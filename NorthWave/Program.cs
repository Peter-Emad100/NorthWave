using Microsoft.EntityFrameworkCore;
using NorthWave.BLL.Discounts;
using NorthWave.BLL.Interfaces;
using NorthWave.BLL.Services;
using NorthWave.DAL;
using NorthWave.DAL.Interfaces;
using NorthWave.DAL.Repositories;
using NorthWave.Middlewares;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
namespace NorthWave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<IDiscountStrategy, RegularDiscountStrategy>();
            builder.Services.AddScoped<IDiscountStrategy, VipDiscountStrategy>();
            builder.Services.AddScoped<IDiscountStrategy, WholesaleDiscountStrategy>();
            builder.Services.AddScoped<IDiscountStrategy, EmployeeDiscountStrategy>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
