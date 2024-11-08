using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.LineUpDb
{
    /// <inheritdoc />
    public partial class LineUpv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineUpBase",
                columns: table => new
                {
                    LineUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    LineUpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineUpType = table.Column<int>(type: "int", nullable: false),
                    MatchType = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    StadiumBackGroud = table.Column<int>(type: "int", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineUpBase", x => x.LineUpId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerLineUp",
                columns: table => new
                {
                    PlayerLineUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineUpId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    PlayerPosition = table.Column<int>(type: "int", nullable: false),
                    IsCaptain = table.Column<bool>(type: "bit", nullable: false),
                    PlayTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerLineUp", x => x.PlayerLineUpId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineUpBase");

            migrationBuilder.DropTable(
                name: "PlayerLineUp");
        }
    }
}
