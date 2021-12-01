using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class AddColumn_CommisionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommisionType",
                table: "OcProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommisionType",
                table: "OcProducts");
        }
    }
}
