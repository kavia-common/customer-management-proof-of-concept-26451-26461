using AutoMapper;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Interfaces;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = request.Request.FirstName.Trim(),
            LastName = request.Request.LastName.Trim(),
            Email = request.Request.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(request.Request.Phone) ? null : request.Request.Phone.Trim(),
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        await _repo.AddAsync(customer, cancellationToken);

        return _mapper.Map<CustomerDto>(customer);
    }
}
