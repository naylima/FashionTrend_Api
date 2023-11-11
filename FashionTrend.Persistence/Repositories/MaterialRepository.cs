using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
{
	public MaterialRepository(AppDbContext context) : base(context)
    {
	}

    public async Task<Material> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Materials.FirstOrDefaultAsync(
            m => m.Name.Equals(name), cancellationToken);
    }
}

