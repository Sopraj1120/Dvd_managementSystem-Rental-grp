using dvdrental.DTOs.RequestDtos;
using dvdrental.Entity;

namespace dvdrental.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(Guid id);
        Task<bool> UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Guid id);
    }
}
