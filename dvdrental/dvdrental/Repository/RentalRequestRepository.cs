using dvdrental.Entity;
using dvdrental.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace dvdrental.Repository
{
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly string _connectionString;

       public RentalRequestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<RentalRequest> AddRentalRequest(RentalRequest rental)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"
                    INSERT INTO RentalRequests (Id, MovieId, MovieTitle, MovieImage, AvailableCopies, CustomerId, Status, RentDate, ReturnDate)
                    VALUES (@Id, @MovieId, @MovieTitle, @MovieImage, @AvailableCopies, @CustomerId, @Status, @RentDate, @ReturnDate)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", rental.Id);
                        command.Parameters.AddWithValue("@MovieId", rental.MovieId);
                        command.Parameters.AddWithValue("@MovieTitle", rental.MovieTitle);
                        command.Parameters.AddWithValue("@MovieImage", rental.MovieImage);
                        command.Parameters.AddWithValue("@AvailableCopies", rental.AvailableCopies);
                        command.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                        command.Parameters.AddWithValue("@Status", rental.Status);
                        command.Parameters.AddWithValue("@RentDate", rental.RentDate);
                        command.Parameters.AddWithValue("@ReturnDate", rental.ReturnDate);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return rental;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<RentalRequest>> GetAllRentals()
        {
            var rentalRequests = new List<RentalRequest>();
            string selectQuery = "SELECT * FROM RentalRequests";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(selectQuery, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var rentalRequest = new RentalRequest
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                MovieId = reader.GetGuid(reader.GetOrdinal("MovieId")),
                                MovieTitle = reader.IsDBNull(reader.GetOrdinal("MovieTitle")) ? null : reader.GetString(reader.GetOrdinal("MovieTitle")),
                                MovieImage = reader.IsDBNull(reader.GetOrdinal("MovieImage")) ? null : reader.GetString(reader.GetOrdinal("MovieImage")),
                                AvailableCopies = reader.GetInt32(reader.GetOrdinal("AvailableCopies")),
                                CustomerId = reader.GetGuid(reader.GetOrdinal("CustomerId")),
                                RentDate = reader.GetString(reader.GetOrdinal("RentDate")), 
                                ReturnDate = reader.GetString(reader.GetOrdinal("ReturnDate")), 
                            };

                           
                            if (Enum.TryParse<RentalRequest.RentalStatus>(reader.GetString(reader.GetOrdinal("Status")), out var status))
                            {
                                rentalRequest.Status = status;
                            }
                            else
                            {
                                rentalRequest.Status = RentalRequest.RentalStatus.Pending; 
                            }

                            rentalRequests.Add(rentalRequest);
                        }
                    }
                }
            }

            return rentalRequests;
        }

        public async Task<RentalRequest> UpdateRentals(RentalRequest request)
        {
            string updateQuery = @"
        UPDATE RentalRequests
        SET 
            MovieId = @MovieId,
            MovieTitle = @MovieTitle,
            MovieImage = @MovieImage,
            AvailableCopies = @AvailableCopies,
            CustomerId = @CustomerId,
            Status = @Status,
            RentDate = @RentDate,
            ReturnDate = @ReturnDate
        WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(updateQuery, connection))
                {
                    
                    command.Parameters.AddWithValue("@Id", request.Id);
                    command.Parameters.AddWithValue("@MovieId", request.MovieId);
                    command.Parameters.AddWithValue("@MovieTitle", (object)request.MovieTitle ?? DBNull.Value); 
                    command.Parameters.AddWithValue("@MovieImage", (object)request.MovieImage ?? DBNull.Value);
                    command.Parameters.AddWithValue("@AvailableCopies", request.AvailableCopies);
                    command.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                    command.Parameters.AddWithValue("@Status", request.Status.ToString()); 
                    command.Parameters.AddWithValue("@RentDate", request.RentDate); 
                    command.Parameters.AddWithValue("@ReturnDate", request.ReturnDate); 

                    
                    var rowsAffected = await command.ExecuteNonQueryAsync();

                   
                    if (rowsAffected > 0)
                    {
                        return request; 
                    }
                    else
                    {
                      
                        throw new Exception("Update failed, no rows affected.");
                    }
                }
            }
        }


        public async Task<RentalRequest> GetById(Guid id)
        {
            RentalRequest rentalRequest = null; 

            string selectQuery = "SELECT * FROM RentalRequests WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id); 

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) 
                        {
                            rentalRequest = new RentalRequest
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                MovieId = reader.GetGuid(reader.GetOrdinal("MovieId")),
                                MovieTitle = reader.IsDBNull(reader.GetOrdinal("MovieTitle")) ? null : reader.GetString(reader.GetOrdinal("MovieTitle")),
                                MovieImage = reader.IsDBNull(reader.GetOrdinal("MovieImage")) ? null : reader.GetString(reader.GetOrdinal("MovieImage")),
                                AvailableCopies = reader.IsDBNull(reader.GetOrdinal("AvailableCopies")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AvailableCopies")),
                                CustomerId = reader.GetGuid(reader.GetOrdinal("CustomerId")),
                                Status = Enum.TryParse<RentalRequest.RentalStatus>(reader.GetString(reader.GetOrdinal("Status")), out var status) ? status : RentalRequest.RentalStatus.Pending,
                                RentDate = reader.IsDBNull(reader.GetOrdinal("RentDate")) ? null : reader.GetString(reader.GetOrdinal("RentDate")),
                                ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? null : reader.GetString(reader.GetOrdinal("ReturnDate")),
                            };
                        }
                    }
                }
            }

            return rentalRequest; 
        }

        public async Task<IEnumerable<RentalRequest>> GetByCustomerId(Guid customerId)
        {
            var rentalRequests = new List<RentalRequest>(); 

            string selectQuery = "SELECT * FROM RentalRequests WHERE CustomerId = @CustomerId"; 

            using (var connection = new SqlConnection(_connectionString)) 
            {
                await connection.OpenAsync(); 

                using (var command = new SqlCommand(selectQuery, connection)) 
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId); 

                    using (var reader = await command.ExecuteReaderAsync()) 
                    {
                        while (await reader.ReadAsync()) 
                        {
                            rentalRequests.Add(new RentalRequest 
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                MovieId = reader.GetGuid(reader.GetOrdinal("MovieId")),
                                MovieTitle = reader.IsDBNull(reader.GetOrdinal("MovieTitle")) ? null : reader.GetString(reader.GetOrdinal("MovieTitle")),
                                MovieImage = reader.IsDBNull(reader.GetOrdinal("MovieImage")) ? null : reader.GetString(reader.GetOrdinal("MovieImage")),
                                AvailableCopies = reader.IsDBNull(reader.GetOrdinal("AvailableCopies")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AvailableCopies")),
                                CustomerId = reader.GetGuid(reader.GetOrdinal("CustomerId")),
                                Status = Enum.TryParse<RentalRequest.RentalStatus>(reader.GetString(reader.GetOrdinal("Status")), out var status) ? status : RentalRequest.RentalStatus.Pending,
                                RentDate = reader.IsDBNull(reader.GetOrdinal("RentDate")) ? null : reader.GetDateTime(reader.GetOrdinal("RentDate")).ToString("o"), 
                                ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? null : reader.GetDateTime(reader.GetOrdinal("ReturnDate")).ToString("o") 
                            });
                        }
                    }
                }
            }

            return rentalRequests; 
        }


    }
}
