using Ardalis.Specification;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Interfaces;

/// <summary>
/// Read-only abstraction for customer querying using Ardalis.Specification.
/// </summary>
public interface ICustomerReadRepository : IReadRepositoryBase<Customer>
{
}
