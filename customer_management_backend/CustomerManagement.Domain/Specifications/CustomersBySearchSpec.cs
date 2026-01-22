using Ardalis.Specification;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Domain.Specifications;

/// <summary>
/// Customer filtering spec (no paging) used for count queries.
/// Uses AsNoTracking for read-only access.
/// </summary>
public sealed class CustomersBySearchSpec : Specification<Customer>
{
    public CustomersBySearchSpec(string? q)
    {
        Query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToLowerInvariant();

            Query.Where(c =>
                ((c.FirstName + " " + c.LastName).ToLower().Contains(term)) ||
                (c.Email.ToLower().Contains(term)) ||
                (c.Phone != null && c.Phone.ToLower().Contains(term)));
        }
    }
}
