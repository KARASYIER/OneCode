using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class renameCommision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "OcShops");

            migrationBuilder.RenameColumn(
                name: "CommissionDoing",
                table: "OcShops",
                newName: "CommisionDoing");

            migrationBuilder.RenameColumn(
                name: "CommissionAvailable",
                table: "OcShops",
                newName: "CommisionAvailable");

            migrationBuilder.RenameColumn(
                name: "CommissionApplying",
                table: "OcShops",
                newName: "CommisionApplying");

            migrationBuilder.RenameColumn(
                name: "CommissionRate",
                table: "OcShopProducts",
                newName: "CommisionRate");

            migrationBuilder.RenameColumn(
                name: "CommissionRate",
                table: "OcProducts",
                newName: "CommisionRate");

            migrationBuilder.RenameColumn(
                name: "TotalCommission",
                table: "OcOrders",
                newName: "TotalCommision");

            migrationBuilder.RenameColumn(
                name: "CommissionRate",
                table: "OcOrderDetails",
                newName: "CommisionRate");

            migrationBuilder.RenameColumn(
                name: "Commission",
                table: "OcOrderDetails",
                newName: "Commision");

            migrationBuilder.AddColumn<decimal>(
                name: "CommisionRate",
                table: "OcShops",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDate",
                table: "OcOrders",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPorfit",
                table: "OcOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommisionRate",
                table: "OcShops");

            migrationBuilder.DropColumn(
                name: "TotalPorfit",
                table: "OcOrders");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "OcOrderDetails");

            migrationBuilder.RenameColumn(
                name: "CommisionDoing",
                table: "OcShops",
                newName: "CommissionDoing");

            migrationBuilder.RenameColumn(
                name: "CommisionAvailable",
                table: "OcShops",
                newName: "CommissionAvailable");

            migrationBuilder.RenameColumn(
                name: "CommisionApplying",
                table: "OcShops",
                newName: "CommissionApplying");

            migrationBuilder.RenameColumn(
                name: "CommisionRate",
                table: "OcShopProducts",
                newName: "CommissionRate");

            migrationBuilder.RenameColumn(
                name: "CommisionRate",
                table: "OcProducts",
                newName: "CommissionRate");

            migrationBuilder.RenameColumn(
                name: "TotalCommision",
                table: "OcOrders",
                newName: "TotalCommission");

            migrationBuilder.RenameColumn(
                name: "CommisionRate",
                table: "OcOrderDetails",
                newName: "CommissionRate");

            migrationBuilder.RenameColumn(
                name: "Commision",
                table: "OcOrderDetails",
                newName: "Commission");

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "OcShops",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedDate",
                table: "OcOrders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
