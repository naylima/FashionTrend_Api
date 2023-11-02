using System;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Persistence.Context;

namespace FashionTrend.Persistence.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public async Task Commit(CancellationToken cancellationToken)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}

