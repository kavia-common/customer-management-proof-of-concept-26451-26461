using AutoMapper;
using CustomerManagement.Domain.Entities;

namespace CustomerManagement.Application.Customers;

public sealed class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerDto>();
    }
}
