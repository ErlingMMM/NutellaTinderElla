using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutellaTinderElla.Migrations
{
    /// <inheritdoc />
    public partial class TinderEF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReleaseYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Trailer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FranchiseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movie_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovie",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovie", x => new { x.CharacterId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovie_Movie_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Character",
                columns: new[] { "Id", "Alias", "FullName", "Gender", "Picture" },
                values: new object[,]
                {
                    { 1, "Becca", "Becca Crane", "Female", "https://images6.fanpop.com/image/polls/1170000/1170146_1358549057630_full.jpg" },
                    { 2, "Kantmiss", "Kantmiss Evershot", "Female", "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg" },
                    { 3, "Cindy", "Cindy Campbell", "Female", "https://i2-prod.dailystar.co.uk/incoming/article24438061.ece/ALTERNATES/s1200c/0_Scary-Movie-cast-now.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The Twilight Saga is a series of romantic fantasy novels and films that chronicle the love story between a teenage girl named Becca Crane and a vampire named Edward Sullen, set against a backdrop of supernatural creatures and a love triangle with a werewolf named Jacob White.", "The Twilight Saga" },
                    { 2, "The Hunger Games is a dystopian science fiction series that follows the courageous journey of Kantmiss Evershot as she becomes an unexpected symbol of rebellion in a brutal society where children are forced to fight to the death in a televised spectacle.", "The Hunger Games" },
                    { 3, "'Scream' is a horror film franchise that cleverly deconstructs and pays homage to the conventions of the slasher genre while keeping audiences on the edge of their seats with its iconic Ghostface killer and suspenseful storytelling.", "Scream" }
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Director", "FranchiseId", "Genre", "Picture", "ReleaseYear", "Title", "Trailer" },
                values: new object[,]
                {
                    { 1, "Jason Friedberg", 1, "Comedy, Fantasy, Parody", "https://prod-ripcut-delivery.disney-plus.net/v1/variant/disney/6C91AACD4A3AD8A7594BDB731F42EB2F78AA5472ECFB6517613D68FCCA146B9F/scale?width=1200&aspectRatio=1.78&format=jpeg", "2010", "Vampires Suck", "https://www.youtube.com/watch?v=Gw1oGbdXzoU" },
                    { 2, "Jason Friedberg", 2, "Action, Sci-Fi, Parody", "https://m.media-amazon.com/images/M/MV5BMTgwOTk2OTY4MV5BMl5BanBnXkFtZTgwMTU5MjE0MDE@._V1_FMjpg_UX1000_.jpg", "2013", "The Starving Games", "https://m.media-amazon.com/images/M/MV5BZTM0NjU0OTctZWI2Ny00NjMwLTg1OWQtMjcwNTJmMjVkMDBmXkEyXkFqcGdeQXVyNDgyNzAxMzY@._V1_.jpg" },
                    { 3, "Keenen Ivory Wayans", 3, "Comedy, Parody", "https://i-viaplay-com.akamaized.net/viaplay-prod/227/368/1613846837-a57c63e0114da2586105e974441375b190f88934.jpg?width=400&height=600", "2000", "Scary Movie", "https://www.youtube.com/watch?v=HTLPULt0eJ4" },
                    { 4, "Keenen Ivory Wayans", 3, "Comedy, Parody", "https://m.media-amazon.com/images/M/MV5BMzQxYjU1OTUtYjRiOC00NDg2LWI4MWUtZGU5YzdkYTcwNTBlXkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_FMjpg_UX1000_.jpg", "2001", "Scary Movie 2", "https://www.youtube.com/watch?v=zCFZUZxBVuI" },
                    { 5, "David Zucker", 3, "Comedy, Parody", "https://m.media-amazon.com/images/I/816m+orOPPL._AC_UF894,1000_QL80_.jpg", "2003", "Scary Movie 3", "https://www.youtube.com/watch?v=O21wD8Tzr2k" }
                });

            migrationBuilder.InsertData(
                table: "CharacterMovie",
                columns: new[] { "CharacterId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_FranchiseId",
                table: "Movie",
                column: "FranchiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterMovie");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Franchise");
        }
    }
}
