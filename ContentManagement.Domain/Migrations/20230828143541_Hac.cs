using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class Hac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HacForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false),
                    ClosestAssociationId = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SwedenIdentificationNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TurkeyIdentificationNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    PassportGivenDate = table.Column<DateTime>(type: "date", nullable: false),
                    PassportGivenPlace = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PassportExpirationDate = table.Column<DateTime>(type: "date", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DepartureAirport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LandingAirport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PassportPicture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HeadshotPicture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HacForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HacForms_Association",
                        column: x => x.ClosestAssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HacForms_Gender",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HacForms_MaritalStatus",
                        column: x => x.MaritalStatusId,
                        principalTable: "MaritalStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HacForms_RoomType",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_ClosestAssociationId",
                table: "HacForms",
                column: "ClosestAssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_GenderId",
                table: "HacForms",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_MaritalStatusId",
                table: "HacForms",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_HacForms_RoomTypeId",
                table: "HacForms",
                column: "RoomTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HacForms");
        }
    }
}
