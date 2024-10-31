using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.PlayerDb
{
    /// <inheritdoc />
    public partial class Playerv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "player");

            migrationBuilder.CreateTable(
                name: "ClubPlayers",
                schema: "player",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerNationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlayerImage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    PlayerAge = table.Column<int>(type: "int", nullable: false),
                    PlayerValue = table.Column<double>(type: "float", nullable: false),
                    PlayerHealth = table.Column<int>(type: "int", nullable: false),
                    PlayerSkill = table.Column<int>(type: "int", nullable: false),
                    PlayerSalary = table.Column<double>(type: "float", nullable: false),
                    Shirtnumber = table.Column<int>(type: "int", nullable: false),
                    PlayerStatus = table.Column<int>(type: "int", nullable: false),
                    leg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    height = table.Column<double>(type: "float", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubPlayers", x => x.PlayerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubPlayers",
                schema: "player");
        }
    }
}
