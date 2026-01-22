using Ardalis.Specification;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Specifications;

/// <summary>
/// Customers list query with optional case-insensitive search and paging.
/// Uses AsNoTracking for read-only access.
/// </summary>
public sealed class CustomersBySearchAndPagingSpec : Specification<Customer>
{
    public CustomersBySearchAndPagingSpec(string? q, int page, int pageSize)
    {
        Query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToLowerInvariant();

            // NOTE: SQLite is case-insensitive by default for ASCII when using LIKE,
            // but to preserve existing behavior we normalize to lower-case on both sides.
            Query.Where(c =>
                ((c.FirstName + " " + c.LastName).ToLower().Contains(term)) ||
                (c.Email.ToLower().Contains(term)) ||
                (c.Phone != null && c.Phone.ToLower().Contains(term)));
        }

        // Requirement: order by Name (we approximate by FirstName then LastName).
        Query.OrderBy(c => c.FirstName).ThenBy(c => c.LastName);

        var safePage = page <= 0 ? 1 : page;
        var safePageSize = pageSize <= 0 ? 20 : pageSize;

        Query.Skip((safePage - 1) * safePageSize).Take(safePageSize);
    }
}
