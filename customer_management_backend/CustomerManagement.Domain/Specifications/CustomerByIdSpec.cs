using Ardalis.Specification;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Specifications;

/// <summary>
/// Fetch a single customer by Id (read-only / AsNoTracking).
/// </summary>
public sealed class CustomerByIdSpec : Specification<Customer>, ISingleResultSpecification<Customer>
{
    public CustomerByIdSpec(Guid id)
    {
        Query.AsNoTracking();
        Query.Where(c => c.Id == id);
    }
}
