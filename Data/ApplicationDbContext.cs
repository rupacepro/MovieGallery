using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieGallery.Models;

namespace MovieGallery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = "A", Name = "Action" },
                new Genre { GenreId = "C", Name = "Comedy" },
                new Genre { GenreId = "D", Name = "Drama" },
                new Genre { GenreId = "H", Name = "Horror" },
                new Genre { GenreId = "M", Name = "Musical" },
                new Genre { GenreId = "R", Name = "RomCom" },
                new Genre { GenreId = "S", Name = "SciFi" }
            );

            // Seed data for Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    MovieId = 1,
                    Name = "Casablanca",
                    Year = 1942,
                    Rating = 5,
                    GenreId = "D",
                    ReleaseDate = new DateTime(1942, 11, 26),
                    Director = "Michael Curtiz",
                    Duration = 102
                },
                new Movie
                {
                    MovieId = 2,
                    Name = "Wonder Woman",
                    Year = 2017,
                    Rating = 3,
                    GenreId = "A",
                    ReleaseDate = new DateTime(2017, 6, 2),
                    Director = "Patty Jenkins",
                    Duration = 141
                },
                new Movie
                {
                    MovieId = 3,
                    Name = "Moonstruck",
                    Year = 1988,
                    Rating = 4,
                    GenreId = "R",
                    ReleaseDate = new DateTime(1988, 1, 15),
                    Director = "Norman Jewison",
                    Duration = 102
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    Content = "This movie is fantastic, loved it!",
                    CreatedAt = DateTime.UtcNow,
                    UserId = "7f8ca75a-80c6-4790-87f8-a230519eff38",
                    MovieId = 1
                },
                new Review
                {
                    Id = 2,
                    Content = "I will rate this movie 5 out of 5.",
                    CreatedAt = DateTime.UtcNow,
                    UserId = "8b5cd3b9-650e-4909-8734-75a3bcec7fac",
                    MovieId = 1
                }
             );
        }
    }
}
