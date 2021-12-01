using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_SettlementAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPayAmount",
                table: "OcOrders",
                newName: "SettlementAmount");

            migrationBuilder.RenameColumn(
                name: "RemainPayAmount",
                table: "OcOrderDetails",
                newName: "RemainSettlementAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SettlementAmount",
                table: "OcOrders",
                newName: "TotalPayAmount");

            migrationBuilder.RenameColumn(
                name: "RemainSettlementAmount",
                table: "OcOrderDetails",
                newName: "RemainPayAmount");
        }
    }
}
