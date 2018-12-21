using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Felix.Azure.MvcMovie.Entity;

namespace Felix.Azure.MvcMovie.DARepositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MvcMovieContext _context;

        public MovieRepository(MvcMovieContext context)
        {
            _context = context;
        }

        public async Task<IList<string>> GetGenreAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            return await genreQuery.Distinct().ToListAsync();
        }

        public async Task<List<Movie>> GetMovies(string movieGenre, string searchString)
        {
            var movies = from m in _context.Movie
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return await movies.ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int? id)
        {
            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);

            return movie;
        }

        public async Task SaveAsync(Movie movie)
        {
            if (_context.Movie.Any(x => x.ID == movie.ID))
            {
                _context.Update(movie);
            }
            else
            {
                _context.Add(movie);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }
    }
}
