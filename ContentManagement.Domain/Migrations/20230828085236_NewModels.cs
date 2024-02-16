using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContentManagement.Domain.Migrations
{
    public partial class NewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosestAssociation",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "PassportGivenPlace",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "UmreForms");

            migrationBuilder.RenameColumn(
                name: "Nationalitiy",
                table: "UmreForms",
                newName: "Nationality");

            migrationBuilder.AlterColumn<string>(
                name: "PassportPicture",
                table: "UmreForms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "HeadshotPicture",
                table: "UmreForms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ClosestAssociationId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatusId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomTypeId",
                table: "UmreForms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypes = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_ClosestAssociationId",
                table: "UmreForms",
                column: "ClosestAssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_GenderId",
                table: "UmreForms",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_MaritalStatusId",
                table: "UmreForms",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UmreForms_RoomTypeId",
                table: "UmreForms",
                column: "RoomTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_Association",
                table: "UmreForms",
                column: "ClosestAssociationId",
                principalTable: "Associations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_Gender",
                table: "UmreForms",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_MaritalStatus",
                table: "UmreForms",
                column: "MaritalStatusId",
                principalTable: "MaritalStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UmreForms_RoomType",
                table: "UmreForms",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_Association",
                table: "UmreForms");

            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_Gender",
                table: "UmreForms");

            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_MaritalStatus",
                table: "UmreForms");

            migrationBuilder.DropForeignKey(
                name: "FK_UmreForms_RoomType",
                table: "UmreForms");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "MaritalStatus");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_ClosestAssociationId",
                table: "UmreForms");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_GenderId",
                table: "UmreForms");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_MaritalStatusId",
                table: "UmreForms");

            migrationBuilder.DropIndex(
                name: "IX_UmreForms_RoomTypeId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "ClosestAssociationId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "MaritalStatusId",
                table: "UmreForms");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "UmreForms");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "UmreForms",
                newName: "Nationalitiy");

            migrationBuilder.AlterColumn<string>(
                name: "PassportPicture",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "HeadshotPicture",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "ClosestAssociation",
                table: "UmreForms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UmreForms",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "UmreForms",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportGivenPlace",
                table: "UmreForms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "UmreForms",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
