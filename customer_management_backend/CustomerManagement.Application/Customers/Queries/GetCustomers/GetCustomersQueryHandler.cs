using AutoMapper;
using CustomerManagement.Domain.Interfaces;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetCustomers;

public sealed class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedCustomersResponse>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PagedCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page <= 0 ? 1 : request.Page;
        var pageSize = request.PageSize is <= 0 or > 200 ? 20 : request.PageSize;

        var (items, totalCount) = await _repo.GetPagedAsync(page, pageSize, request.Search, cancellationToken);

        return new PagedCustomersResponse
        {
            Items = items.Select(c => _mapper.Map<CustomerDto>(c)).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
