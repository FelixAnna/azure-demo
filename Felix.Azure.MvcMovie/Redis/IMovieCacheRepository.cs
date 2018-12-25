using System.Collections.Generic;
using System.Threading.Tasks;
using Felix.Azure.MvcMovie.Entity;

namespace Felix.Azure.MvcMovie.Redis
{
    public interface IMovieCacheRepository
    {
        Task<IList<string>> GetGenreAsync();
        Task SetGenreAsync(IEnumerable<string> genres);
        Task SetMoviesAsync(IEnumerable<Movie> movies);
            Task<Movie> GetMovieByIdAsync(int? id);
        Task<List<Movie>> GetMovies(string movieGenre, string searchString);
        Task RemoveAsync(int id);
        Task SaveAsync(Movie movie);
    }
}