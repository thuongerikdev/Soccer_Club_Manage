using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.TournamentDb
{
    /// <inheritdoc />
    public partial class Tourv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ClubPlayers_UserID",
                schema: "tournament",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserID",
                schema: "tournament",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TournamentID",
                schema: "tournament",
                table: "Orders",
                column: "TournamentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TournamentBase_TournamentID",
                schema: "tournament",
                table: "Orders",
                column: "TournamentID",
                principalSchema: "tournament",
                principalTable: "TournamentBase",
                principalColumn: "TournamentID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TournamentBase_TournamentID",
                schema: "tournament",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TournamentID",
                schema: "tournament",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                schema: "tournament",
                table: "Orders",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ClubPlayers_UserID",
                schema: "tournament",
                table: "Orders",
                column: "UserID",
                principalSchema: "tournament",
                principalTable: "ClubPlayers",
                principalColumn: "PlayerID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
