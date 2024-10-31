using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.ClubDb
{
    /// <inheritdoc />
    public partial class Clubv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "club");

            migrationBuilder.CreateTable(
                name: "ClubTeam",
                schema: "club",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubLogo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubBanner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Budget = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubTeam", x => x.ClubId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubTeam",
                schema: "club");
        }
    }
}
