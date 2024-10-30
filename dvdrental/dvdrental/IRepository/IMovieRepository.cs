using dvdrental.Entity;

namespace dvdrental.IRepository
{

    public interface IMovieRepository
    {

        Task<Movies> AddMovie(Movies movie);
        Task<Movies> GetMovieById(int id);
        Task<List<Movies>> GetAllMovies();
        Task<List<Movies>> GetMoviesByCategory(Guid categoryId);
        Task<bool> UpdateMovie(Movies movie);
        Task<bool> DeleteMovie(Guid id);


    }
}
