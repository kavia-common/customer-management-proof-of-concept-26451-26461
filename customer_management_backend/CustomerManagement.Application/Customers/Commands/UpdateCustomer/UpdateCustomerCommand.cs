using MediatR;

namespace CustomerManagement.Application.Customers.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid Id, UpdateCustomerRequest Request) : IRequest<CustomerDto?>;
