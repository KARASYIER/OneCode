using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_RefundAmountAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayAmount",
                table: "OcOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RefundBalance",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainAmount",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainPayAmount",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPayAmount",
                table: "OcOrders");

            migrationBuilder.DropColumn(
                name: "RefundBalance",
                table: "OcOrderDetails");

            migrationBuilder.DropColumn(
                name: "RemainAmount",
                table: "OcOrderDetails");

            migrationBuilder.DropColumn(
                name: "RemainPayAmount",
                table: "OcOrderDetails");
        }
    }
}
