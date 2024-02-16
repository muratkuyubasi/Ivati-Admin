using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class DebtorDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {  

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Debtors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Debtors",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Debtors");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Debtors");
        }
    }
}
