using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.TournamentDb
{
    /// <inheritdoc />
    public partial class Tourv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubTeam_ClubPlayers_UserID",
                schema: "tournament",
                table: "ClubTeam");

            migrationBuilder.DropIndex(
                name: "IX_ClubTeam_UserID",
                schema: "tournament",
                table: "ClubTeam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ClubTeam_UserID",
                schema: "tournament",
                table: "ClubTeam",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubTeam_ClubPlayers_UserID",
                schema: "tournament",
                table: "ClubTeam",
                column: "UserID",
                principalSchema: "tournament",
                principalTable: "ClubPlayers",
                principalColumn: "PlayerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
