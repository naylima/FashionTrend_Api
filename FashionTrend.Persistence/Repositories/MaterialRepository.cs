using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;

namespace FashionTrend.Persistence.Repositories;

public class MaterialRepository : BaseRepository<Material>, IMaterialRepository
{
	public MaterialRepository(AppDbContext context) : base(context)
    {
	}
}

