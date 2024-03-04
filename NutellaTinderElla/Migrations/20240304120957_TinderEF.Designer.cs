﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMovieApi.Data;

#nullable disable

namespace NutellaTinderElla.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20240304120957_TinderEF")]
    partial class TinderEF
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebMovieApi.Data.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Character");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "Becca",
                            FullName = "Becca Crane",
                            Gender = "Female",
                            Picture = "https://images6.fanpop.com/image/polls/1170000/1170146_1358549057630_full.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "Kantmiss",
                            FullName = "Kantmiss Evershot",
                            Gender = "Female",
                            Picture = "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "Cindy",
                            FullName = "Cindy Campbell",
                            Gender = "Female",
                            Picture = "https://i2-prod.dailystar.co.uk/incoming/article24438061.ece/ALTERNATES/s1200c/0_Scary-Movie-cast-now.jpg"
                        });
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.CharacterMovie", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CharacterMovie", (string)null);

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            MoviesId = 1
                        },
                        new
                        {
                            CharacterId = 2,
                            MoviesId = 2
                        },
                        new
                        {
                            CharacterId = 3,
                            MoviesId = 3
                        },
                        new
                        {
                            CharacterId = 3,
                            MoviesId = 4
                        },
                        new
                        {
                            CharacterId = 3,
                            MoviesId = 5
                        });
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Franchise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "The Twilight Saga is a series of romantic fantasy novels and films that chronicle the love story between a teenage girl named Becca Crane and a vampire named Edward Sullen, set against a backdrop of supernatural creatures and a love triangle with a werewolf named Jacob White.",
                            Name = "The Twilight Saga"
                        },
                        new
                        {
                            Id = 2,
                            Description = "The Hunger Games is a dystopian science fiction series that follows the courageous journey of Kantmiss Evershot as she becomes an unexpected symbol of rebellion in a brutal society where children are forced to fight to the death in a televised spectacle.",
                            Name = "The Hunger Games"
                        },
                        new
                        {
                            Id = 3,
                            Description = "'Scream' is a horror film franchise that cleverly deconstructs and pays homage to the conventions of the slasher genre while keeping audiences on the edge of their seats with its iconic Ghostface killer and suspenseful storytelling.",
                            Name = "Scream"
                        });
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ReleaseYear")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Trailer")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Movie");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Jason Friedberg",
                            FranchiseId = 1,
                            Genre = "Comedy, Fantasy, Parody",
                            Picture = "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/6C91AACD4A3AD8A7594BDB731F42EB2F78AA5472ECFB6517613D68FCCA146B9F/scale?width=1200&aspectRatio=1.78&format=jpeg",
                            ReleaseYear = "2010",
                            Title = "Vampires Suck",
                            Trailer = "https://www.youtube.com/watch?v=Gw1oGbdXzoU"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Jason Friedberg",
                            FranchiseId = 2,
                            Genre = "Action, Sci-Fi, Parody",
                            Picture = "https://m.media-amazon.com/images/M/MV5BMTgwOTk2OTY4MV5BMl5BanBnXkFtZTgwMTU5MjE0MDE@._V1_FMjpg_UX1000_.jpg",
                            ReleaseYear = "2013",
                            Title = "The Starving Games",
                            Trailer = "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Director = "Keenen Ivory Wayans",
                            FranchiseId = 3,
                            Genre = "Comedy, Parody",
                            Picture = "https://i-viaplay-com.akamaized.net/viaplay-prod/227/368/1613846837-a57c63e0114da2586105e974441375b190f88934.jpg?width=400&height=600",
                            ReleaseYear = "2000",
                            Title = "Scary Movie",
                            Trailer = "https://www.youtube.com/watch?v=HTLPULt0eJ4"
                        },
                        new
                        {
                            Id = 4,
                            Director = "Keenen Ivory Wayans",
                            FranchiseId = 3,
                            Genre = "Comedy, Parody",
                            Picture = "https://m.media-amazon.com/images/M/MV5BMzQxYjU1OTUtYjRiOC00NDg2LWI4MWUtZGU5YzdkYTcwNTBlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg",
                            ReleaseYear = "2001",
                            Title = "Scary Movie 2",
                            Trailer = "https://www.youtube.com/watch?v=zCFZUZxBVuI"
                        },
                        new
                        {
                            Id = 5,
                            Director = "David Zucker",
                            FranchiseId = 3,
                            Genre = "Comedy, Parody",
                            Picture = "https://m.media-amazon.com/images/I/816m+orOPPL._AC_UF894,1000_QL80_.jpg",
                            ReleaseYear = "2003",
                            Title = "Scary Movie 3",
                            Trailer = "https://www.youtube.com/watch?v=O21wD8Tzr2k"
                        });
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.CharacterMovie", b =>
                {
                    b.HasOne("WebMovieApi.Data.Models.Character", "Characters")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMovieApi.Data.Models.Movie", "Movies")
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characters");

                    b.Navigation("Movies");
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.Movie", b =>
                {
                    b.HasOne("WebMovieApi.Data.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("WebMovieApi.Data.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}