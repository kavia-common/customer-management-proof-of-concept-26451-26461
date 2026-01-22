using MediatR;

namespace CustomerManagement.Application.Customers.Commands.CreateCustomer;

public sealed record CreateCustomerCommand(CreateCustomerRequest Request) : IRequest<CustomerDto>;
