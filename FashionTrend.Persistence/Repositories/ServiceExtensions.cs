using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Persistence.Context;
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
	}
}

