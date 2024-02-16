using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "UserModel");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDualNationality",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "UserModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDualNationality",
                table: "UserModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Spouses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Spouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderId",
                table: "Users",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserModel_GenderId",
                table: "UserModel",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_GenderId",
                table: "Spouses",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserModel_Genders_GenderId",
                table: "UserModel",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserModel_Genders_GenderId",
                table: "UserModel");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Genders_GenderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GenderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserModel_GenderId",
                table: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_Spouses_GenderId",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDualNationality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "IsDualNationality",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Spouses");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Spouses");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "UserModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
