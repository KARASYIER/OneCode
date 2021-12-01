using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_ShopProduct_CommisionTypeAndValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommisionType",
                table: "OcShopProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CommisionValue",
                table: "OcShopProducts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommisionType",
                table: "OcShopProducts");

            migrationBuilder.DropColumn(
                name: "CommisionValue",
                table: "OcShopProducts");
        }
    }
}
