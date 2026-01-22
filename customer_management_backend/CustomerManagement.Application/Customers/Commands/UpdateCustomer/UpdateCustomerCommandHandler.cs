using AutoMapper;
using CustomerManagement.Domain.Interfaces;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto?>
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CustomerDto?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return null;
        }

        customer.FirstName = request.Request.FirstName.Trim();
        customer.LastName = request.Request.LastName.Trim();
        customer.Email = request.Request.Email.Trim();
        customer.Phone = string.IsNullOrWhiteSpace(request.Request.Phone) ? null : request.Request.Phone.Trim();
        customer.UpdatedAt = DateTimeOffset.UtcNow;

        await _repo.UpdateAsync(customer, cancellationToken);

        return _mapper.Map<CustomerDto>(customer);
    }
}
