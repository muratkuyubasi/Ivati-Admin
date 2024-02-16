using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class FrontMenuName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrontMenuName",
                table: "FrontMenuRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrontMenuName",
                table: "FrontMenuRecords");
        }
    }
}
