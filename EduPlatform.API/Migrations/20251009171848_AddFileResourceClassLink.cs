using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFileResourceClassLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Files_FileResourceId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Profs_ProfId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Profs_Email",
                table: "Profs");

            migrationBuilder.DropIndex(
                name: "IX_Profs_Slug",
                table: "Profs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FileResources");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ProfId",
                table: "FileResources",
                newName: "IX_FileResources_ProfId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "FileResources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(180)",
                oldMaxLength: 180);

            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "FileResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "FileResources",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProfId1",
                table: "FileResources",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileResources",
                table: "FileResources",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FileResources_ClassroomId",
                table: "FileResources",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_FileResources_ProfId1",
                table: "FileResources",
                column: "ProfId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FileResources_FileResourceId",
                table: "Comments",
                column: "FileResourceId",
                principalTable: "FileResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileResources_Classrooms_ClassroomId",
                table: "FileResources",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileResources_Profs_ProfId",
                table: "FileResources",
                column: "ProfId",
                principalTable: "Profs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileResources_Profs_ProfId1",
                table: "FileResources",
                column: "ProfId1",
                principalTable: "Profs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FileResources_FileResourceId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileResources_Classrooms_ClassroomId",
                table: "FileResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FileResources_Profs_ProfId",
                table: "FileResources");

            migrationBuilder.DropForeignKey(
                name: "FK_FileResources_Profs_ProfId1",
                table: "FileResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileResources",
                table: "FileResources");

            migrationBuilder.DropIndex(
                name: "IX_FileResources_ClassroomId",
                table: "FileResources");

            migrationBuilder.DropIndex(
                name: "IX_FileResources_ProfId1",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "ProfId1",
                table: "FileResources");

            migrationBuilder.RenameTable(
                name: "FileResources",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_FileResources_ProfId",
                table: "Files",
                newName: "IX_Files_ProfId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Comments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Files",
                type: "nvarchar(180)",
                maxLength: 180,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Profs_Email",
                table: "Profs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profs_Slug",
                table: "Profs",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Files_FileResourceId",
                table: "Comments",
                column: "FileResourceId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Profs_ProfId",
                table: "Files",
                column: "ProfId",
                principalTable: "Profs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
