using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.TournamentDb
{
    /// <inheritdoc />
    public partial class Tourv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchesStatisticId",
                schema: "tournament",
                table: "MatchesStatistic",
                newName: "MatchesStatisticID");

            migrationBuilder.RenameColumn(
                name: "Score",
                schema: "tournament",
                table: "MatchesStatistic",
                newName: "YellowCard");

            migrationBuilder.AddColumn<int>(
                name: "Assist",
                schema: "tournament",
                table: "MatchesStatistic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
                schema: "tournament",
                table: "MatchesStatistic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RedCard",
                schema: "tournament",
                table: "MatchesStatistic",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assist",
                schema: "tournament",
                table: "MatchesStatistic");

            migrationBuilder.DropColumn(
                name: "Goal",
                schema: "tournament",
                table: "MatchesStatistic");

            migrationBuilder.DropColumn(
                name: "RedCard",
                schema: "tournament",
                table: "MatchesStatistic");

            migrationBuilder.RenameColumn(
                name: "MatchesStatisticID",
                schema: "tournament",
                table: "MatchesStatistic",
                newName: "MatchesStatisticId");

            migrationBuilder.RenameColumn(
                name: "YellowCard",
                schema: "tournament",
                table: "MatchesStatistic",
                newName: "Score");
        }
    }
}
