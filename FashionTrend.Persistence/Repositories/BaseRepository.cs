using System;
using FashionTrend.Domain.Entities;
using FashionTrend.Domain.Interfaces;
using FashionTrend.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FashionTrend.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext context;

    public BaseRepository(AppDbContext context)
    {
        this.context = context;
    }

    public void Create(T entity)
    {
        entity.DateCreated = DateTimeOffset.Now;
        context.Add(entity);
    }

    public void Delete(T entity)
    {
        entity.DateDeleted = DateTimeOffset.Now;
        context.Remove(entity);
    }

    public async Task<T> Get(Guid id, CancellationToken cancellationToken) =>
        await context.Set<T>().FirstOrDefaultAsync(
            t => t.Id.Equals(id), cancellationToken);

    public async Task<List<T>> GetAll(CancellationToken cancellationToken) =>
        await context.Set<T>().ToListAsync(cancellationToken);
    

    public void Update(T entity)
    {
        entity.DateUpdated = DateTimeOffset.Now;
        context.Update(entity);
    }
}

