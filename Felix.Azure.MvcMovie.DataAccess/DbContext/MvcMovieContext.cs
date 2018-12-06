using Felix.Azure.MvcMovie.Entity;
using Microsoft.EntityFrameworkCore;

namespace Felix.Azure.MvcMovie.DataAccess
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
