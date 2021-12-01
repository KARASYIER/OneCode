using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OneCode.Migrations
{
    public partial class Rename_CommisionRecord_SalerIdAndSalerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "OcCommisionRecords");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "OcCommisionRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "SalerId",
                table: "OcCommisionRecords",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SalerName",
                table: "OcCommisionRecords",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalerId",
                table: "OcCommisionRecords");

            migrationBuilder.DropColumn(
                name: "SalerName",
                table: "OcCommisionRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "OcCommisionRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "OcCommisionRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
