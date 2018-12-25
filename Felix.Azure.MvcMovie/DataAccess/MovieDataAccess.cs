using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Felix.Azure.MvcMovie.Entity;
using Felix.Azure.MvcMovie.Redis;
using Felix.Azure.MvcMovie.Repositories;

namespace Felix.Azure.MvcMovie.DataAccess
{
    public class MovieDataAccess : IMovieDataAccess
    {
        private readonly IMovieCacheRepository _movieCache;
        private readonly IMovieRepository _movieRepo;

        public MovieDataAccess(IMovieCacheRepository movieCache, IMovieRepository movieRepo)
        {
            _movieCache = movieCache;
            _movieRepo = movieRepo;
        }

        public async Task<IList<string>> GetGenreAsync()
        {
            var results = await _movieCache.GetGenreAsync();
            if (results == null)
            {
                results = await _movieRepo.GetGenreAsync();
                await _movieCache.SetGenreAsync(results);
            }

            return results;
        }

        public async Task<Movie> GetMovieByIdAsync(int? id)
        {
            var result = await _movieCache.GetMovieByIdAsync(id);
            if (result == null)
            {
                result = await _movieRepo.GetMovieByIdAsync(id);
                await _movieCache.SaveAsync(result);
            }

            return result;
        }

        public async Task<List<Movie>> GetMovies(string movieGenre, string searchString)
        {
            var results = await _movieCache.GetMovies(movieGenre, searchString);
            if (results == null)
            {
                results = await _movieRepo.GetMovies(movieGenre, searchString);
                await _movieCache.SetMoviesAsync(await _movieRepo.GetMovies(null, null));
            }

            return results;
        }

        public async Task RemoveAsync(int id)
        {
            await _movieCache.RemoveAsync(id);
            await _movieRepo.RemoveAsync(id);
        }

        public async Task SaveAsync(Movie movie)
        {
            await _movieCache.SaveAsync(movie);
            await _movieRepo.SaveAsync(movie);
        }
    }
}
