using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dvdrental.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

    
        public async Task<CustomerResponseDto> AddCustomer(CustomerRequestDto customer)
        {
            try
            {
                if (customer.Password != customer.ConfirmPassword)
                {
                    throw new ArgumentException("Password and ConfirmPassword do not match");
                }

                var user = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Email = customer.Email,
                    Password = customer.Password,
                    MobileNo = customer.MobileNo,
                    NicNo = customer.NicNo,
                    UserName = customer.UserName,
                    IsActive = "true"
                };

                var createCustomer = await _repo.AddCustomer(user);

                return new CustomerResponseDto
                {
                    Id = createCustomer.Id,
                    FirstName = createCustomer.FirstName,
                    LastName = createCustomer.LastName,
                    Address = createCustomer.Address,
                    Email = createCustomer.Email,
                    NicNo = createCustomer.NicNo,
                    MobileNo = createCustomer.MobileNo,
                    UserName = createCustomer.UserName
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomers()
        {
            try
            {
                var customers = await _repo.GetAllCustomers();

                var customerDtos = new List<CustomerResponseDto>();

                foreach (var customer in customers)
                {
                    customerDtos.Add(new CustomerResponseDto
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Address = customer.Address,
                        NicNo = customer.NicNo,
                        MobileNo = customer.MobileNo,
                        UserName = customer.UserName,
                        Password = customer.Password
                    });
                }

                return customerDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

       
        public async Task<CustomerResponseDto> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _repo.GetCustomerById(id);
                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                return new CustomerResponseDto
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Address = customer.Address,
                    NicNo = customer.NicNo,
                    MobileNo = customer.MobileNo,
                    UserName = customer.UserName,
                    Password = customer.Password,
                    ConfirmPassword=customer.Password
                };
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        
        public async Task<CustomerResponseDto> UpdateCustomer(Guid id, CustomerRequestDto customer)
        {
            try
            {
              
                if (customer.Password != customer.ConfirmPassword)
                {
                    throw new ArgumentException("Password and ConfirmPassword do not match");
                }

                
                var existingCustomer = await _repo.GetCustomerById(id);
                if (existingCustomer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

              
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Address = customer.Address;
                existingCustomer.Password = customer.Password;
                existingCustomer.NicNo = customer.NicNo;
                existingCustomer.MobileNo = customer.MobileNo;
                existingCustomer.UserName = customer.UserName;

                
                var updateSuccess = await _repo.UpdateCustomer(existingCustomer);
                if (!updateSuccess)
                {
                    throw new Exception("Failed to update the customer");
                }

               
                return new CustomerResponseDto
                {
                    Id = existingCustomer.Id,
                    FirstName = existingCustomer.FirstName,
                    LastName = existingCustomer.LastName,
                    Email = existingCustomer.Email,
                    Address = existingCustomer.Address,
                    NicNo = existingCustomer.NicNo,
                    MobileNo = existingCustomer.MobileNo,
                    UserName = existingCustomer.UserName
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        
        public async Task<bool> DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await _repo.GetCustomerById(id);
                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                return await _repo.DeleteCustomer(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
