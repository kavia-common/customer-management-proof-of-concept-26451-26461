using AutoMapper;
using CustomerManagement.Domain.Interfaces;
using CustomerManagement.Domain.Specifications;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetCustomers;

public sealed class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedCustomersResponse>
{
    private readonly ICustomerReadRepository _readRepo;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(ICustomerReadRepository readRepo, IMapper mapper)
    {
        _readRepo = readRepo;
        _mapper = mapper;
    }

    public async Task<PagedCustomersResponse> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var page = request.Page <= 0 ? 1 : request.Page;
        var pageSize = request.PageSize is <= 0 or > 200 ? 20 : request.PageSize;

        // List using paging spec.
        var listSpec = new CustomersBySearchAndPagingSpec(request.Search, page, pageSize);
        var items = await _readRepo.ListAsync(listSpec, cancellationToken);

        // Count using matching filter spec (no paging).
        var countSpec = new CustomersBySearchSpec(request.Search);
        var totalCount = await _readRepo.CountAsync(countSpec, cancellationToken);

        return new PagedCustomersResponse
        {
            Items = items.Select(c => _mapper.Map<CustomerDto>(c)).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}
