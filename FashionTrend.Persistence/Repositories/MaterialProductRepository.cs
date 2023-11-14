using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class MaterialProductRepository : BaseRepository<MaterialProduct>, IMaterialProductRepository
{
	public MaterialProductRepository(AppDbContext context) : base(context)
    {
	}
}

