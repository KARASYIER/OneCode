using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class Rename_DisplayOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DispalyOrder",
                table: "OcShopProducts");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "OcShopProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "OcShopProducts");

            migrationBuilder.AddColumn<int>(
                name: "DispalyOrder",
                table: "OcShopProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
