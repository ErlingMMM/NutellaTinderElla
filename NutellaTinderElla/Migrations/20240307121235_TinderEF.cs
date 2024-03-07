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
                name: "CurrentUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GenderPreference = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Seeking = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikerId = table.Column<int>(type: "int", nullable: false),
                    LikedUserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.LikerId, x.LikedUserId });
                    table.ForeignKey(
                        name: "FK_Likes_CurrentUser_LikedUserId",
                        column: x => x.LikedUserId,
                        principalTable: "CurrentUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_CurrentUser_LikerId",
                        column: x => x.LikerId,
                        principalTable: "CurrentUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Swipes",
                columns: table => new
                {
                    SwiperId = table.Column<int>(type: "int", nullable: false),
                    SwipedUserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swipes", x => new { x.SwiperId, x.SwipedUserId });
                    table.ForeignKey(
                        name: "FK_Swipes_CurrentUser_SwipedUserId",
                        column: x => x.SwipedUserId,
                        principalTable: "CurrentUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Swipes_CurrentUser_SwiperId",
                        column: x => x.SwiperId,
                        principalTable: "CurrentUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CurrentUser",
                columns: new[] { "Id", "Age", "Bio", "Email", "Gender", "GenderPreference", "Name", "PhoneNumber", "Picture", "Seeking" },
                values: new object[,]
                {
                    { 1, 28, "Passionate about travel, cooking, and outdoor adventures. Looking for someone to explore new cuisines and hiking trails with. Let's create memories together!", "emily.johnson@example.com", 1, 1, "Emily Johnson", "555-123-4567", "emily_profile_picture.jpg", 0 },
                    { 2, 32, "Tech enthusiast, coffee lover, and avid reader. Seeking meaningful connections and engaging conversations. Let's explore the city together!", "john.smith@example.com", 0, 2, "John Smith", "555-987-6543", "john_profile_picture.jpg", 1 },
                    { 3, 25, "Passionate about art, music, and nature. Looking for someone who shares similar interests and enjoys spontaneous adventures.", "olivia.brown@example.com", 1, 0, "Olivia Brown", "555-555-5555", "olivia_profile_picture.jpg", 1 },
                    { 4, 30, "Fitness enthusiast, foodie, and travel addict. Seeking someone to join me on my next hiking trip and explore new culinary delights.", "michael.johnson@example.com", 0, 1, "Michael Johnson", "555-222-3333", "michael_profile_picture.jpg", 1 },
                    { 5, 27, "Bookworm, animal lover, and adventure seeker. Looking for someone who values intellectual conversations and enjoys outdoor activities.", "sophia.martinez@example.com", 1, 0, "Sophia Martinez", "555-777-8888", "sophia_profile_picture.jpg", 0 },
                    { 6, 29, "Gamer, movie buff, and pizza enthusiast. Seeking someone to binge-watch Netflix series and share a slice of pizza with.", "daniel.wilson@example.com", 0, 2, "Daniel Wilson", "555-444-5555", "daniel_profile_picture.jpg", 0 },
                    { 7, 26, "Nature lover, coffee addict, and amateur photographer. Seeking someone who appreciates sunsets, coffee dates, and long walks in the park.", "emma.taylor@example.com", 1, 1, "Emma Taylor", "555-999-1111", "emma_profile_picture.jpg", 1 },
                    { 8, 31, "Tech geek, foodie, and aspiring chef. Looking for someone who enjoys experimenting with new recipes and binge-watching sci-fi movies.", "liam.anderson@example.com", 0, 0, "Liam Anderson", "555-333-2222", "liam_profile_picture.jpg", 0 },
                    { 9, 28, "Music lover, traveler, and coffee connoisseur. Seeking someone who enjoys spontaneous road trips, live music, and lazy Sundays.", "ava.thomas@example.com", 1, 2, "Ava Thomas", "555-666-9999", "ava_profile_picture.jpg", 1 },
                    { 10, 29, "Adventure seeker, adrenaline junkie, and thrill-seeker. Looking for someone who shares a passion for extreme sports and outdoor adventures.", "ethan.walker@example.com", 0, 1, "Ethan Walker", "555-111-7777", "ethan_profile_picture.jpg", 1 },
                    { 11, 26, "Art enthusiast, wine lover, and aspiring painter. Seeking someone who appreciates creativity, fine wine, and deep conversations.", "isabella.garcia@example.com", 1, 0, "Isabella Garcia", "555-888-2222", "isabella_profile_picture.jpg", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikedUserId",
                table: "Likes",
                column: "LikedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Swipes_SwipedUserId",
                table: "Swipes",
                column: "SwipedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Swipes");

            migrationBuilder.DropTable(
                name: "CurrentUser");
        }
    }
}
