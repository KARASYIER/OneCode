using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class UpdateForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers");

            migrationBuilder.AddForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers",
                column: "ShopId",
                principalTable: "OcShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers");

            migrationBuilder.AddForeignKey(
                name: "FK_OcSalers_OcShops_ShopId",
                table: "OcSalers",
                column: "ShopId",
                principalTable: "OcShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
