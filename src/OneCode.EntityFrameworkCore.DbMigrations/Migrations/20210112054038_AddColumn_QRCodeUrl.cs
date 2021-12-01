using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_QRCodeUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "OcShops",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "OcSalers",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "OcShops");

            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "OcSalers");
        }
    }
}
