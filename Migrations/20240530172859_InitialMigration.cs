using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcaTento.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTeamOne = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    NameTeamTwo = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    Slug = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    ImageTeamOne = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    ImageTeamTwo = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    ScoreTotal = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    ScoreTeamOne = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    ScoreTeamTwo = table.Column<int>(type: "INT", maxLength: 10, nullable: false),
                    MatchDate = table.Column<string>(type: "NVARCHAR(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Matches",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_Slug",
                table: "Match",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_UserId",
                table: "Match",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Slug",
                table: "User",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
