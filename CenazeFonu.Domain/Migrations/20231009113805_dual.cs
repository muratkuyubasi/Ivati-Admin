using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenazeFonu.Domain.Migrations
{
    public partial class dual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDualNationality",
                table: "UserModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDualNationality",
                table: "UserModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
