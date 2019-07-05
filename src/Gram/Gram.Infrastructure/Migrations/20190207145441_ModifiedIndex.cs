using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Infrastructure.Migrations
{
    public partial class ModifiedIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_GeneralType_Title",
                schema: "General",
                table: "GeneralType");

            migrationBuilder.CreateIndex(
                name: "UQ_GeneralType_Title_Parent",
                schema: "General",
                table: "GeneralType",
                columns: new[] { "Title", "ParentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_GeneralType_Title_Parent",
                schema: "General",
                table: "GeneralType");

            migrationBuilder.CreateIndex(
                name: "UQ_GeneralType_Title",
                schema: "General",
                table: "GeneralType",
                column: "Title",
                unique: true);
        }
    }
}
