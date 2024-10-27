using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SM.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthRole",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRole", x => x.roleId);
                });

            migrationBuilder.CreateTable(
                name: "AuthUser",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUser", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "AuthRolePermission",
                columns: table => new
                {
                    rolePermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    permissionKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthRolePermission", x => x.rolePermissionId);
                    table.ForeignKey(
                        name: "FK_AuthRolePermission_AuthRole_roleId",
                        column: x => x.roleId,
                        principalTable: "AuthRole",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuthUserRole",
                columns: table => new
                {
                    userRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthUserRole", x => x.userRoleId);
                    table.ForeignKey(
                        name: "FK_AuthUserRole_AuthRole_roleId",
                        column: x => x.roleId,
                        principalTable: "AuthRole",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthUserRole_AuthUser_userId",
                        column: x => x.userId,
                        principalTable: "AuthUser",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthRolePermission_roleId",
                table: "AuthRolePermission",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRole_roleId",
                table: "AuthUserRole",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthUserRole_userId",
                table: "AuthUserRole",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthRolePermission");

            migrationBuilder.DropTable(
                name: "AuthUserRole");

            migrationBuilder.DropTable(
                name: "AuthRole");

            migrationBuilder.DropTable(
                name: "AuthUser");
        }
    }
}
