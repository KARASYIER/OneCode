using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_Order_SalerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalerName",
                table: "OcOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopName",
                table: "OcOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalerName",
                table: "OcOrders");

            migrationBuilder.DropColumn(
                name: "ShopName",
                table: "OcOrders");
        }
    }
}
