using CustomerManagement.Domain.Interfaces;
using MediatR;

namespace CustomerManagement.Application.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _repo;

    public DeleteCustomerCommandHandler(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return;
        }

        await _repo.DeleteAsync(customer, cancellationToken);
    }
}
