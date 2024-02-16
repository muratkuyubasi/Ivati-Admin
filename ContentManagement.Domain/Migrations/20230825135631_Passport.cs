using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class Passport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassportGivenPlace",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportGivenPlace",
                table: "UmreForms");
        }
    }
}
