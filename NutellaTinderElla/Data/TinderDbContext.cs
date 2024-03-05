using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Models;

namespace NutellaTinderEllaApi.Data
{
    public class TinderDbContext : DbContext
    {
        public TinderDbContext(DbContextOptions<TinderDbContext> options) : base(options)
        {
        }

        //Tables to be made when migration
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        public DbSet<CurrentUser> CurrentUser { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=N-NO-01-01-6434\\SQLEXPRESS; Initial Catalog=TinderEF; Integrated Security= true; Trust Server Certificate= true;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Franchises
            modelBuilder.Entity<Franchise>().HasData(
                new Franchise { Id = 1, Name = "The Twilight Saga", Description = "The Twilight Saga is a series of romantic fantasy novels and films that chronicle the love story between a teenage girl named Becca Crane and a vampire named Edward Sullen, set against a backdrop of supernatural creatures and a love triangle with a werewolf named Jacob White." },
                new Franchise { Id = 2, Name = "The Hunger Games", Description = "The Hunger Games is a dystopian science fiction series that follows the courageous journey of Kantmiss Evershot as she becomes an unexpected symbol of rebellion in a brutal society where children are forced to fight to the death in a televised spectacle." },
                new Franchise { Id = 3, Name = "Scream", Description = "'Scream' is a horror film franchise that cleverly deconstructs and pays homage to the conventions of the slasher genre while keeping audiences on the edge of their seats with its iconic Ghostface killer and suspenseful storytelling." });

            modelBuilder.Entity<CurrentUser>().HasData(
                new CurrentUser
                {
                    Id = 1,
                    Name = "Emily Johnson",
                    Age = 28,
                    Email = "emily.johnson@example.com",
                    Bio = "Passionate about travel, cooking, and outdoor adventures. Looking for someone to explore new cuisines and hiking trails with. Let's create memories together!",
                    Gender = 1,
                    GenderPreference = 1,
                    Seeking = 0,
                    PhoneNumber = "555-123-4567",
                    Picture = "emily_profile_picture.jpg"
                });


            //Movies
            modelBuilder.Entity<Movie>().HasOne(m => m.Franchise).WithMany(f => f.Movies).HasForeignKey(m => m.FranchiseId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Vampires Suck", Genre = "Comedy, Fantasy, Parody", ReleaseYear = "2010", Director = "Jason Friedberg", Picture = "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/6C91AACD4A3AD8A7594BDB731F42EB2F78AA5472ECFB6517613D68FCCA146B9F/scale?width=1200&aspectRatio=1.78&format=jpeg", Trailer = "https://www.youtube.com/watch?v=Gw1oGbdXzoU", FranchiseId = 1 },
                new Movie { Id = 2, Title = "The Starving Games", Genre = "Action, Sci-Fi, Parody", ReleaseYear = "2013", Director = "Jason Friedberg", Picture = "https://m.media-amazon.com/images/M/MV5BMTgwOTk2OTY4MV5BMl5BanBnXkFtZTgwMTU5MjE0MDE@._V1_FMjpg_UX1000_.jpg", Trailer = "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg", FranchiseId = 2 },
                new Movie { Id = 3, Title = "Scary Movie", Genre = "Comedy, Parody", ReleaseYear = "2000", Director = "Keenen Ivory Wayans", Picture = "https://i-viaplay-com.akamaized.net/viaplay-prod/227/368/1613846837-a57c63e0114da2586105e974441375b190f88934.jpg?width=400&height=600", Trailer = "https://www.youtube.com/watch?v=HTLPULt0eJ4", FranchiseId = 3 },
                new Movie { Id = 4, Title = "Scary Movie 2", Genre = "Comedy, Parody", ReleaseYear = "2001", Director = "Keenen Ivory Wayans", Picture = "https://m.media-amazon.com/images/M/MV5BMzQxYjU1OTUtYjRiOC00NDg2LWI4MWUtZGU5YzdkYTcwNTBlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg", Trailer = "https://www.youtube.com/watch?v=zCFZUZxBVuI", FranchiseId = 3 },
                new Movie { Id = 5, Title = "Scary Movie 3", Genre = "Comedy, Parody", ReleaseYear = "2003", Director = "David Zucker", Picture = "https://m.media-amazon.com/images/I/816m+orOPPL._AC_UF894,1000_QL80_.jpg", Trailer = "https://www.youtube.com/watch?v=O21wD8Tzr2k", FranchiseId = 3 });

            //Characters
            modelBuilder.Entity<Character>().HasData(
               new Character { Id = 1, FullName = "Becca Crane", Alias = "Becca", Gender = "Female", Picture = "https://images6.fanpop.com/image/polls/1170000/1170146_1358549057630_full.jpg" },
               new Character { Id = 2, FullName = "Kantmiss Evershot", Alias = "Kantmiss", Gender = "Female", Picture = "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg" },
               new Character { Id = 3, FullName = "Cindy Campbell", Alias = "Cindy", Gender = "Female", Picture = "https://i2-prod.dailystar.co.uk/incoming/article24438061.ece/ALTERNATES/s1200c/0_Scary-Movie-cast-now.jpg" });

            //CharacterMovies
            modelBuilder.Entity<CharacterMovie>().HasKey(cm => new { cm.CharacterId, cm.MoviesId });


            //CharacterMovies
            modelBuilder.Entity<Character>()
                .HasMany(left => left.Movies)
                .WithMany(right => right.Characters)
                .UsingEntity<CharacterMovie>(
                    right => right.HasOne(e => e.Movies).WithMany(),
                    left => left.HasOne(e => e.Characters).WithMany().HasForeignKey(e => e.CharacterId),
                    join => join.ToTable("CharacterMovie")
                );

            modelBuilder.Entity<CharacterMovie>().HasData(
                new CharacterMovie() { CharacterId = 1, MoviesId = 1 },
                new CharacterMovie() { CharacterId = 2, MoviesId = 2 },
                new CharacterMovie() { CharacterId = 3, MoviesId = 3 },
                new CharacterMovie() { CharacterId = 3, MoviesId = 4 },
                new CharacterMovie() { CharacterId = 3, MoviesId = 5 });
        }
    }
}
