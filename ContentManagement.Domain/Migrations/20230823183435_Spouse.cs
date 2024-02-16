using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class Spouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HusbandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spouses_FamilyMember",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spouses_Users",
                        column: x => x.HusbandId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_FamilyMemberId",
                table: "Spouses",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_HusbandId",
                table: "Spouses",
                column: "HusbandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spouses");
        }
    }
}
