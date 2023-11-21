using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionTrend.Persistence.Repositories;

public static class ServiceExtensions
{
	public static void ConfigurePersistenceApp(
		this IServiceCollection service, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("Sqlite");

		service.AddDbContext<AppDbContext>(
			opt => opt.UseSqlite(connectionString));

		service.AddScoped<IUnitOfWork, UnitOfWork>();
		service.AddScoped<ISupplierRepository, SupplierRepository>();
		service.AddScoped<IProductRepository, ProductRepository>();
		service.AddScoped<IMaterialRepository, MaterialRepository>();
		service.AddScoped<IOrderRepository, OrderRepository>();
		service.AddScoped<IContractRepository, ContractRepository>();
		service.AddScoped<IPaymentRepository, PaymentRepository>();
		service.AddScoped<IKafkaProducer, KafkaProducer>();
	}
}

