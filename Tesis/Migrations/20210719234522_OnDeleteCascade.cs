using Microsoft.EntityFrameworkCore.Migrations;

namespace Tesis.Migrations
{
    public partial class OnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Properties_PropertyId",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Properties_PropertyId",
                table: "Photos",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Properties_PropertyId",
                table: "Photos");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Properties_PropertyId",
                table: "Photos",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
