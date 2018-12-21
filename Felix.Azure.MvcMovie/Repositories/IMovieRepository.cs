using System.Collections.Generic;
using System.Threading.Tasks;
using Felix.Azure.MvcMovie.Entity;

namespace Felix.Azure.MvcMovie.Repositories
{
    public interface IMovieRepository
    {
        Task<IList<string>> GetGenreAsync();
        Task<Movie> GetMovieByIdAsync(int? id);
        Task<List<Movie>> GetMovies(string movieGenre, string searchString);
        Task SaveAsync(Movie movie);
        Task RemoveAsync(int id);
    }
}