
using Felix.Azure.MvcMovie.Entity;
using Felix.Azure.MvcMovie.Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Felix.Azure.MvcMovie
{
    public static class SeedMovieDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(
                         new Movie
                         {
                             Title = "When Harry Met Sally",
                             ReleaseDate = DateTime.Parse("1989-1-11"),
                             Genre = "Romantic Comedy",
                             Rating = 9.0M,
                             Price = 7.99M
                         },

                         new Movie
                         {
                             Title = "Ghostbusters ",
                             ReleaseDate = DateTime.Parse("1984-3-13"),
                             Genre = "Comedy",
                             Rating = 7.8M,
                             Price = 8.99M
                         },

                         new Movie
                         {
                             Title = "Ghostbusters 2",
                             ReleaseDate = DateTime.Parse("1986-2-23"),
                             Genre = "Comedy",
                             Rating = 8.1M,
                             Price = 9.99M
                         },

                       new Movie
                       {
                           Title = "Rio Bravo",
                           ReleaseDate = DateTime.Parse("1959-4-15"),
                           Genre = "Western",
                           Rating = 2.9M,
                           Price = 3.99M
                       }
                    );
                }

                if (!context.Actor.Any())
                {
                    context.Actor.AddRange(
                            new Actor()
                            {
                                Name = "Dora",
                                Gender = false,
                                Birthday = new DateTime(1989, 07, 11),
                                Description = "Dora Dora"
                            },
                            new Actor()
                            {
                                Name = "Diyago",
                                Gender = true,
                                Birthday = new DateTime(1989, 07, 11),
                                Description = "Dora Dora"
                            }
                        );
                }
                context.SaveChanges();
            }
        }
    }
}
