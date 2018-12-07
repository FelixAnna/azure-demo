using Felix.Azure.MvcMovie.Entity;
using Microsoft.EntityFrameworkCore;
using Felix.Azure.MvcMovie.Entity.Model;

namespace Felix.Azure.MvcMovie
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Felix.Azure.MvcMovie.Entity.Model.Actor> Actor { get; set; }
    }
}
