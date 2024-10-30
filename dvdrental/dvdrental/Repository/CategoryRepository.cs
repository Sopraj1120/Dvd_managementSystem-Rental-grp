using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;

namespace dvdrental.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> AddCategory(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "INSERT INTO Categories (Name) VALUES (@name)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", category.Name);


            await command.ExecuteNonQueryAsync();
            return "Return SuccessFully..";

        }

        public async Task UpdateCategory(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "UPDATE Categories SET Name = @name WHERE Id = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", category.Id);
            command.Parameters.AddWithValue("@name", category.Name);
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteCategory(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "DELETE FROM Categories WHERE Id = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();

        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = new List<Category>();
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Categories";
            using var command = new SqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                categories.Add(new Category
                {
                    Id = reader.GetGuid(0),
                    CategoryId=reader.GetInt32(1),
                    Name = reader.GetString(2)
                });
            }

            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT * FROM Categories WHERE CategoryId = @id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Category
                {
                    Id = reader.GetGuid(0),
                    CategoryId=reader.GetInt32(1),
                    Name = reader.GetString(2)
                };
            }

            return null;
        }
    }
}
