using System;
namespace FashionTrend.Domain.Interfaces;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationTokeb);
}

