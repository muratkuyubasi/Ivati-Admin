using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class PostCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "UmreForms",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "HacForms",
                type: "nvarchar(max)",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "HacForms");
        }
    }
}
