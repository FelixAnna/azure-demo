using Felix.Azure.MvcMovie.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Felix.Azure.MvcMovie.DataAccess
{
    public interface IMovieDataAccess
    {
        Task<IList<string>> GetGenreAsync();
        Task<Movie> GetMovieByIdAsync(int? id);
        Task<List<Movie>> GetMovies(string movieGenre, string searchString);
        Task SaveAsync(Movie movie);
        Task RemoveAsync(int id);
    }
}
