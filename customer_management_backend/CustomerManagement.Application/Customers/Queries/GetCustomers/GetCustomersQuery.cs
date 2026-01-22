using CustomerManagement.Application.Customers;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetCustomers;

public sealed record GetCustomersQuery(int Page, int PageSize, string? Search) : IRequest<PagedCustomersResponse>;
