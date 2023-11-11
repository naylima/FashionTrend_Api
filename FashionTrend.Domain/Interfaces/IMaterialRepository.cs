using System;
using FashionTrend.Domain.Entities;

namespace FashionTrend.Domain.Interfaces;

public interface IMaterialRepository : IBaseRepository<Material>
{
    Task<Material> GetByName(string name, CancellationToken cancellationToken);
}

