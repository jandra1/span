using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Culturio.Application.Migrations
{
    public partial class addexternalidpropertytouser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "User");
        }
    }
}
