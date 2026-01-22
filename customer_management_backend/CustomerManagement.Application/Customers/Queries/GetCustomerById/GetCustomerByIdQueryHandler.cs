using AutoMapper;
using CustomerManagement.Domain.Interfaces;
using CustomerManagement.Domain.Specifications;
using MediatR;

namespace CustomerManagement.Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    private readonly ICustomerReadRepository _readRepo;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerReadRepository readRepo, IMapper mapper)
    {
        _readRepo = readRepo;
        _mapper = mapper;
    }

    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new CustomerByIdSpec(request.Id);
        var customer = await _readRepo.FirstOrDefaultAsync(spec, cancellationToken);

        return customer is null ? null : _mapper.Map<CustomerDto>(customer);
    }
}
