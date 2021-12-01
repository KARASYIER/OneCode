using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_FromCityToCity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromCity",
                table: "OcProducts",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToCity",
                table: "OcProducts",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromCity",
                table: "OcProducts");

            migrationBuilder.DropColumn(
                name: "ToCity",
                table: "OcProducts");
        }
    }
}
