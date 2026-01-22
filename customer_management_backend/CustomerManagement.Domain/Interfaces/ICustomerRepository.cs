using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Interfaces;

/// <summary>
/// Abstraction for customer persistence.
/// </summary>
public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Customer> Items, int TotalCount)> GetPagedAsync(
        int page,
        int pageSize,
        string? search,
        CancellationToken cancellationToken);

    Task AddAsync(Customer customer, CancellationToken cancellationToken);

    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);

    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);
}
