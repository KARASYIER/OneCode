using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_OrderDetail_Count : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommisionType",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CommisionValue",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OcOrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommisionType",
                table: "OcOrderDetails");

            migrationBuilder.DropColumn(
                name: "CommisionValue",
                table: "OcOrderDetails");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "OcOrderDetails");
        }
    }
}
