using Microsoft.EntityFrameworkCore.Migrations;

namespace Tesis.Migrations
{
    public partial class entityadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trusts",
                columns: table => new
                {
                    SourceUserId = table.Column<int>(type: "int", nullable: false),
                    TrustedUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trusts", x => new { x.SourceUserId, x.TrustedUserId });
                    table.ForeignKey(
                        name: "FK_Trusts_Users_SourceUserId",
                        column: x => x.SourceUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trusts_Users_TrustedUserId",
                        column: x => x.TrustedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trusts_TrustedUserId",
                table: "Trusts",
                column: "TrustedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trusts");
        }
    }
}
