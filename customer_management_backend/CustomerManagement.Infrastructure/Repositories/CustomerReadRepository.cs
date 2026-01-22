using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;
using CustomerManagement.Infrastructure.Persistence;

namespace CustomerManagement.Infrastructure.Repositories;

/// <summary>
/// Customer read repository (specification-based).
/// </summary>
public sealed class CustomerReadRepository : EfRepository<Customer>, ICustomerReadRepository
{
    public CustomerReadRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
