using Microsoft.EntityFrameworkCore;
using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Data.Models;


namespace NutellaTinderEllaApi.Data
{
    public class TinderDbContext : DbContext
    {
        public TinderDbContext(DbContextOptions<TinderDbContext> options) : base(options)
        {
        }

        //Tables to be made when migration
        public DbSet<CurrentUser> CurrentUser { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=N-NO-01-01-6434\\SQLEXPRESS; Initial Catalog=TinderEF; Integrated Security= true; Trust Server Certificate= true;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
                },



                new CurrentUser
                {
                    Id = 2,
                    Name = "John Smith",
                    Age = 32,
                    Email = "john.smith@example.com",
                    Bio = "Tech enthusiast, coffee lover, and avid reader. Seeking meaningful connections and engaging conversations. Let's explore the city together!",
                    Gender = 0,
                    GenderPreference = 2,
                    Seeking = 1,
                    PhoneNumber = "555-987-6543",
                    Picture = "john_profile_picture.jpg"
                },
new CurrentUser
{
    Id = 3,
    Name = "Olivia Brown",
    Age = 25,
    Email = "olivia.brown@example.com",
    Bio = "Passionate about art, music, and nature. Looking for someone who shares similar interests and enjoys spontaneous adventures.",
    Gender = 1,
    GenderPreference = 0,
    Seeking = 1,
    PhoneNumber = "555-555-5555",
    Picture = "olivia_profile_picture.jpg"
},
new CurrentUser
{
    Id = 4,
    Name = "Michael Johnson",
    Age = 30,
    Email = "michael.johnson@example.com",
    Bio = "Fitness enthusiast, foodie, and travel addict. Seeking someone to join me on my next hiking trip and explore new culinary delights.",
    Gender = 0,
    GenderPreference = 1,
    Seeking = 1,
    PhoneNumber = "555-222-3333",
    Picture = "michael_profile_picture.jpg"
},
new CurrentUser
{
    Id = 5,
    Name = "Sophia Martinez",
    Age = 27,
    Email = "sophia.martinez@example.com",
    Bio = "Bookworm, animal lover, and adventure seeker. Looking for someone who values intellectual conversations and enjoys outdoor activities.",
    Gender = 1,
    GenderPreference = 0,
    Seeking = 0,
    PhoneNumber = "555-777-8888",
    Picture = "sophia_profile_picture.jpg"
},
new CurrentUser
{
    Id = 6,
    Name = "Daniel Wilson",
    Age = 29,
    Email = "daniel.wilson@example.com",
    Bio = "Gamer, movie buff, and pizza enthusiast. Seeking someone to binge-watch Netflix series and share a slice of pizza with.",
    Gender = 0,
    GenderPreference = 2,
    Seeking = 0,
    PhoneNumber = "555-444-5555",
    Picture = "daniel_profile_picture.jpg"
},
new CurrentUser
{
    Id = 7,
    Name = "Emma Taylor",
    Age = 26,
    Email = "emma.taylor@example.com",
    Bio = "Nature lover, coffee addict, and amateur photographer. Seeking someone who appreciates sunsets, coffee dates, and long walks in the park.",
    Gender = 1,
    GenderPreference = 1,
    Seeking = 1,
    PhoneNumber = "555-999-1111",
    Picture = "emma_profile_picture.jpg"
},
new CurrentUser
{
    Id = 8,
    Name = "Liam Anderson",
    Age = 31,
    Email = "liam.anderson@example.com",
    Bio = "Tech geek, foodie, and aspiring chef. Looking for someone who enjoys experimenting with new recipes and binge-watching sci-fi movies.",
    Gender = 0,
    GenderPreference = 0,
    Seeking = 0,
    PhoneNumber = "555-333-2222",
    Picture = "liam_profile_picture.jpg"
},
new CurrentUser
{
    Id = 9,
    Name = "Ava Thomas",
    Age = 28,
    Email = "ava.thomas@example.com",
    Bio = "Music lover, traveler, and coffee connoisseur. Seeking someone who enjoys spontaneous road trips, live music, and lazy Sundays.",
    Gender = 1,
    GenderPreference = 2,
    Seeking = 1,
    PhoneNumber = "555-666-9999",
    Picture = "ava_profile_picture.jpg"
},
new CurrentUser
{
    Id = 10,
    Name = "Ethan Walker",
    Age = 29,
    Email = "ethan.walker@example.com",
    Bio = "Adventure seeker, adrenaline junkie, and thrill-seeker. Looking for someone who shares a passion for extreme sports and outdoor adventures.",
    Gender = 0,
    GenderPreference = 1,
    Seeking = 1,
    PhoneNumber = "555-111-7777",
    Picture = "ethan_profile_picture.jpg"
},
new CurrentUser
{
    Id = 11,
    Name = "Isabella Garcia",
    Age = 26,
    Email = "isabella.garcia@example.com",
    Bio = "Art enthusiast, wine lover, and aspiring painter. Seeking someone who appreciates creativity, fine wine, and deep conversations.",
    Gender = 1,
    GenderPreference = 0,
    Seeking = 0,
    PhoneNumber = "555-888-2222",
    Picture = "isabella_profile_picture.jpg"
}
    );


        }
    }
}
