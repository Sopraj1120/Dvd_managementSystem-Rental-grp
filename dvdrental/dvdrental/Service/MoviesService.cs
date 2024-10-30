using dvdrental.DTOs.RequestDtos;
using dvdrental.DTOs.ResponceDtos;
using dvdrental.Entity;
using dvdrental.IRepository;
using dvdrental.IService;

public class MovieService : IMoviesService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieResponseDto> AddMovie(MoviesRequestDto movieRequest)
    {
        var movie = new Movies
        {
            Title = movieRequest.Title,
            Description = movieRequest.Description,
            Copies = movieRequest.Copies,
            CategoryId = movieRequest.CategoryId,
            CategoryName = movieRequest.CategoryName,
            Image = movieRequest.Image,
            IsDeleted = false
        };

        var addedMovie = await _movieRepository.AddMovie(movie);

        return new MovieResponseDto
        {
            Id = addedMovie.Id,
            Title = addedMovie.Title,
            Description = addedMovie.Description,
            Copies = addedMovie.Copies,
            CategoryId = addedMovie.CategoryId,
            CategoryName = addedMovie.CategoryName,
            Image = addedMovie.Image,
            IsDeleted = addedMovie.IsDeleted
        };
    }

    public async Task<List<MovieResponseDto>> GetAllMovies()
    {
        var movies = await _movieRepository.GetAllMovies();
        return movies.Select(item => new MovieResponseDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Copies = item.Copies,
            CategoryId = item.CategoryId,
            CategoryName = item.CategoryName,
            Image = item.Image,
            IsDeleted = item.IsDeleted
        }).ToList();
    }

    public async Task<MovieResponseDto> GetMovieById(int id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null) return null;

        return new MovieResponseDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            Copies = movie.Copies,
            CategoryId = movie.CategoryId,
            CategoryName = movie.CategoryName,
            Image = movie.Image,
            IsDeleted = movie.IsDeleted
        };
    }

    public async Task<bool> UpdateMovie(MoviesRequestDto movieRequest, Guid id)
    {
        var movie = new Movies
        {
            // Make sure you add an Id property to MoviesRequestDto
            Id = id,
            Title = movieRequest.Title,
            Description = movieRequest.Description,
            Copies = movieRequest.Copies,
            CategoryId = movieRequest.CategoryId,
            CategoryName = movieRequest.CategoryName,
            Image = movieRequest.Image,
            IsDeleted = false // or however you want to handle IsDeleted
        };

        return await _movieRepository.UpdateMovie(movie);
    }

    public async Task<bool> DeleteMovie(Guid id)
    {
        return await _movieRepository.DeleteMovie(id);
    }
    public async Task<List<MovieResponseDto>> GetMoviesByCategory(Guid categoryId)
    {
        // Fetch movies from the repository based on the category ID
        var movies = await _movieRepository.GetMoviesByCategory(categoryId);

        // Map to MovieResponceDto
        return movies.Select(item => new MovieResponseDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Copies = item.Copies,
            CategoryId = item.CategoryId,
            CategoryName = item.CategoryName,
            Image = item.Image,
            IsDeleted = item.IsDeleted
        }).ToList();
    }
}
