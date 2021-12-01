using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class Update_Order_SalerId_CanNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "OcProducts",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SalerId",
                table: "OcOrders",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalerId",
                table: "OcCommisionRecords",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_OcProducts_ShopId",
                table: "OcProducts",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_OcProducts_OcShops_ShopId",
                table: "OcProducts",
                column: "ShopId",
                principalTable: "OcShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OcProducts_OcShops_ShopId",
                table: "OcProducts");

            migrationBuilder.DropIndex(
                name: "IX_OcProducts_ShopId",
                table: "OcProducts");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "OcProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "SalerId",
                table: "OcOrders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SalerId",
                table: "OcCommisionRecords",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
