using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dvdrental.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"INSERT INTO Customers (Id , FirstName, LastName, Address, Email, Password, MobileNo, NicNo, UserName, IsActive) 
                                  VALUES (@Id , @FirstName, @LastName, @Address, @Email, @Password, @MobileNo, @NicNo, @UserName, @IsActive)";

                    using (var command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@Password", customer.Password);
                        command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        command.Parameters.AddWithValue("@NicNo", customer.NicNo);
                        command.Parameters.AddWithValue("@UserName", customer.UserName);
                        command.Parameters.AddWithValue("@IsActive", customer.IsActive);

                        await command.ExecuteNonQueryAsync();

                    }
                }
                return customer;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = new List<Customer>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM Customers";
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var customer = new Customer
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNo = reader.GetString(reader.GetOrdinal("MobileNo")),
                                    NicNo = reader.GetString(reader.GetOrdinal("NicNo")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    IsActive = reader.GetString(reader.GetOrdinal("IsActive"))
                                };
                                customers.Add(customer);
                            }
                        }
                    }
                }
                return customers;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<Customer> GetCustomerById(Guid id)
        {
            Customer customer = null;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "SELECT * FROM Customers WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                customer = new Customer
                                {
                                    Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Address = reader.GetString(reader.GetOrdinal("Address")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    MobileNo = reader.GetString(reader.GetOrdinal("MobileNo")),
                                    NicNo = reader.GetString(reader.GetOrdinal("NicNo")),
                                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                    IsActive = reader.GetString(reader.GetOrdinal("IsActive"))
                                };
                            }
                        }
                    }
                }
                return customer;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> UpdateCustomer(Customer customer)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"UPDATE Customers SET 
                                  FirstName = @FirstName, 
                                  LastName = @LastName, 
                                  Address = @Address, 
                                  Email = @Email, 
                                  Password = @Password, 
                                  MobileNo = @MobileNo, 
                                  NicNo = @NicNo, 
                                  UserName = @UserName, 
                                  IsActive = @IsActive 
                                  WHERE Id = @Id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                        command.Parameters.AddWithValue("@LastName", customer.LastName);
                        command.Parameters.AddWithValue("@Address", customer.Address);
                        command.Parameters.AddWithValue("@Email", customer.Email);
                        command.Parameters.AddWithValue("@Password", customer.Password);
                        command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        command.Parameters.AddWithValue("@NicNo", customer.NicNo);
                        command.Parameters.AddWithValue("@UserName", customer.UserName);
                        command.Parameters.AddWithValue("@IsActive", customer.IsActive);
                        command.Parameters.AddWithValue("@Id", customer.Id);

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
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
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "DELETE FROM Customers WHERE Id = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        var rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
