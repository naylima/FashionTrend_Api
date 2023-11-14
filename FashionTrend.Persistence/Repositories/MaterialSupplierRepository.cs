using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;

namespace FashionTrend.Persistence.Repositories;

public class MaterialSupplierRepository : BaseRepository<MaterialSupplier>, IMaterialSupplierRepository
{
	public MaterialSupplierRepository(AppDbContext context) : base(context)
    {
	}
}

