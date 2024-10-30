using dvdrental.DTOs.RequestDtos;
using dvdrental.Entity;
using dvdrental.IService;
using Microsoft.AspNetCore.Mvc;

namespace dvdrental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMoviesService _movieService;

        public MovieController(IMoviesService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MoviesRequestDto movieRequest)
        {
            var movie = await _movieService.AddMovie(movieRequest);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if (movie == null) return NotFound();

            return Ok(movie);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie([FromBody] MoviesRequestDto movieRequest, Guid id)
        {
            var result = await _movieService.UpdateMovie(movieRequest, id);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var result = await _movieService.DeleteMovie(id);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetMoviesByCategory(Guid categoryId)
        {
            var movies = await _movieService.GetMoviesByCategory(categoryId);
            if (movies == null || !movies.Any())
            {
                return NotFound(); // No movies found for this category
            }

            return Ok(movies); // Return the list of movies
        }
    }

}
