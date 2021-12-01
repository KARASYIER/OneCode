using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class RemoveShopCommisionRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers");

            migrationBuilder.DropColumn(
                name: "CommisionRate",
                table: "OcShops");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "OcOrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers",
                column: "ShopId",
                principalTable: "OcShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "OcOrderDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "CommisionRate",
                table: "OcShops",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "OcOrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers",
                column: "ShopId",
                principalTable: "OcShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
