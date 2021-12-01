using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class RenameColumn_FromCityToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCity",
                table: "OcProducts");

            migrationBuilder.DropColumn(
                name: "ToCity",
                table: "OcProducts");

            migrationBuilder.AddColumn<string>(
                name: "CityEnd",
                table: "OcProducts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityStart",
                table: "OcProducts",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityEnd",
                table: "OcProducts");

            migrationBuilder.DropColumn(
                name: "CityStart",
                table: "OcProducts");

            migrationBuilder.AddColumn<string>(
                name: "FromCity",
                table: "OcProducts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToCity",
                table: "OcProducts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
