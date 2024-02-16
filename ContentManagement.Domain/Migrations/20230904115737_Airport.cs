using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class Airport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureAirport",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "LandingAirport",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "DepartureAirport",
                table: "HacForms");

            migrationBuilder.DropColumn(
                name: "LandingAirport",
                table: "HacForms");

            migrationBuilder.AddColumn<int>(
                name: "DepartureAirportId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LandingAirportId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartureAirportId",
                table: "HacForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LandingAirportId",
                table: "HacForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_DepartureAirportId",
                table: "UmreForms",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_LandingAirportId",
                table: "UmreForms",
                column: "LandingAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_DepartureAirportId",
                table: "HacForms",
                column: "DepartureAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_LandingAirportId",
                table: "HacForms",
                column: "LandingAirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_HacForms_DepartureAirport",
                table: "HacForms",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HacForms_LandingAirport",
                table: "HacForms",
                column: "LandingAirportId",
                principalTable: "Airports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_DepartureAirport",
                table: "UmreForms",
                column: "DepartureAirportId",
                principalTable: "Airports",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_LandingAirport",
                table: "UmreForms",
                column: "LandingAirportId",
                principalTable: "Airports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HacForms_DepartureAirport",
                table: "HacForms");

            migrationBuilder.DropForeignKey(
                name: "FK_HacForms_LandingAirport",
                table: "HacForms");

            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_DepartureAirport",
                table: "UmreForms");

            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_LandingAirport",
                table: "UmreForms");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_DepartureAirportId",
                table: "UmreForms");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_LandingAirportId",
                table: "UmreForms");

            migrationBuilder.DropIndex(
                name: "IX_HacForms_DepartureAirportId",
                table: "HacForms");

            migrationBuilder.DropIndex(
                name: "IX_HacForms_LandingAirportId",
                table: "HacForms");

            migrationBuilder.DropColumn(
                name: "DepartureAirportId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "LandingAirportId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "DepartureAirportId",
                table: "HacForms");

            migrationBuilder.DropColumn(
                name: "LandingAirportId",
                table: "HacForms");

            migrationBuilder.AddColumn<string>(
                name: "DepartureAirport",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandingAirport",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureAirport",
                table: "HacForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandingAirport",
                table: "HacForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
