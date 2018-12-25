using Felix.Azure.MvcMovie.Entity;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Felix.Azure.MvcMovie.Redis
{
    public class MovieCacheRepository : IMovieCacheRepository
    {
        private readonly ConnectionMultiplexer _connection;
        public MovieCacheRepository(ConnectionMultiplexer connection)
        {
            _connection = connection;
        }

        public async Task<IList<string>> GetGenreAsync()
        {
            string key = "Genres";
            if (_connection.GetDatabase().KeyExists(key))
            {
                return JsonConvert.DeserializeObject<IList<string>>(await _connection.GetDatabase().StringGetAsync(key));
            }

            return null;
        }

        public async Task SetGenreAsync(IEnumerable<string> genres)
        {
            string key = "Genres";
            if (_connection.GetDatabase().KeyExists(key))
            {
                _connection.GetDatabase().KeyDelete(key);
            }

            _connection.GetDatabase().StringSet(key, JsonConvert.SerializeObject(genres));
        }

        public async Task SetMoviesAsync(IEnumerable<Movie> movies)
        {
            string key = "Movies";
            if (_connection.GetDatabase().KeyExists(key))
            {
                _connection.GetDatabase().KeyDelete(key);
            }

            _connection.GetDatabase().StringSet(key, JsonConvert.SerializeObject(movies));
        }

        public async Task<Movie> GetMovieByIdAsync(int? id)
        {
            string key = "Movies";
            if (_connection.GetDatabase().KeyExists(key))
            {
                return JsonConvert.DeserializeObject<List<Movie>>(await _connection.GetDatabase().StringGetAsync(key)).FirstOrDefault(x => x.ID == id);
            }

            return null;
        }

        public async Task<List<Movie>> GetMovies(string movieGenre, string searchString)
        {
            string key = "Movies";
            if (_connection.GetDatabase().KeyExists(key))
            {
                var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(await _connection.GetDatabase().StringGetAsync(key));
                if (!String.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Title.Contains(searchString));
                }

                if (!String.IsNullOrEmpty(movieGenre))
                {
                    movies = movies.Where(x => x.Genre == movieGenre);
                }

                return movies.ToList();
            }

            return null;
        }

        public async Task SaveAsync(Movie movie)
        {
            string key = "Movies";
            if (_connection.GetDatabase().KeyExists(key))
            {
                var cachedMovies = JsonConvert.DeserializeObject<List<Movie>>(await _connection.GetDatabase().StringGetAsync(key));
                cachedMovies.RemoveAll(x => x.ID == movie.ID);
                cachedMovies.Add(movie);
                _connection.GetDatabase().StringSet(key, JsonConvert.SerializeObject(cachedMovies));
            }
            else
            {
                var movies = new List<Movie> { movie };
                await _connection.GetDatabase().StringSetAsync(key, JsonConvert.SerializeObject(movies));
            }
        }

        public async Task RemoveAsync(int id)
        {
            string key = "Movies";
            if (_connection.GetDatabase().KeyExists(key))
            {
                var cachedMovies = JsonConvert.DeserializeObject<List<Movie>>(await _connection.GetDatabase().StringGetAsync(key));
                cachedMovies.RemoveAll(x => x.ID == id);
                await _connection.GetDatabase().StringSetAsync(key, JsonConvert.SerializeObject(cachedMovies));
            }
        }
    }
}
