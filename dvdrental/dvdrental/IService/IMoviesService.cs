using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;

namespace dvdrental.IService
{
    public interface IMoviesService
    {
        Task<MovieResponseDto> AddMovie(MoviesRequestDto movieRequest);
        Task<List<MovieResponseDto>> GetAllMovies();
        Task<MovieResponseDto> GetMovieById(int id);
        Task<bool> UpdateMovie(MoviesRequestDto movieRequest, Guid id);
        Task<bool> DeleteMovie(Guid id);
        Task<List<MovieResponseDto>> GetMoviesByCategory(Guid categoryId);


    }
}
