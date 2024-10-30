using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> AddCustomer(CustomerRequestDto customer);
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomers();
        Task<CustomerResponseDto> GetCustomerById(Guid id);
        Task<CustomerResponseDto> UpdateCustomer(Guid id, CustomerRequestDto customer);
        Task<bool> DeleteCustomer(Guid id);
    }

}
