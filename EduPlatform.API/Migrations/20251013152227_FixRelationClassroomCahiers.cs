using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduPlatform.API.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationClassroomCahiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CahiersPedagogiques_ClassId",
                table: "CahiersPedagogiques");

            migrationBuilder.CreateIndex(
                name: "IX_CahiersPedagogiques_ClassId",
                table: "CahiersPedagogiques",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CahiersPedagogiques_ClassId",
                table: "CahiersPedagogiques");

            migrationBuilder.CreateIndex(
                name: "IX_CahiersPedagogiques_ClassId",
                table: "CahiersPedagogiques",
                column: "ClassId",
                unique: true);
        }
    }
}
