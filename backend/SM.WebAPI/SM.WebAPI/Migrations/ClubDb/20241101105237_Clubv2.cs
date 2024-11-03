using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations.ClubDb
{
    /// <inheritdoc />
    public partial class Clubv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClubAge",
                schema: "club",
                table: "ClubTeam",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ClubLevel",
                schema: "club",
                table: "ClubTeam",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClubAge",
                schema: "club",
                table: "ClubTeam");

            migrationBuilder.DropColumn(
                name: "ClubLevel",
                schema: "club",
                table: "ClubTeam");
        }
    }
}
