using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;

namespace dvdrental.Repository
{
    public class AdminRepository :IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "INSERT INTO Admins (Id, Email, Password) OUTPUT INSERTED.Id VALUES (@Id, @Email, @Password)";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Guid.NewGuid());
                        command.Parameters.AddWithValue("@Email", admin.Email);
                        command.Parameters.AddWithValue("@Password", admin.Password);

                        var insertedId = await command.ExecuteScalarAsync();

                    }
                }
                return admin;
            }
            catch (SqlException sqlEx)
            {
                
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
           
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }





        public async Task<List<Admin>> GetAllMovies()
        {
            var Admins = new List<Admin>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM Admins";
                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Admins.Add(new Admin
                        {
                            Id=reader.GetGuid(0),
                            Email=reader.GetString(1),
                            Password=reader.GetString(2),
                        });
                    }
                }
            }
            return Admins;
        }


    }
}
