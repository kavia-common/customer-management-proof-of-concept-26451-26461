using Ardalis.Specification.EntityFrameworkCore;
using CustomerManagement.Infrastructure.Persistence;

namespace CustomerManagement.Infrastructure.Repositories;

/// <summary>
/// Generic EF Core repository supporting Ardalis.Specification.
/// </summary>
public class EfRepository<T> : RepositoryBase<T> where T : class
{
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
