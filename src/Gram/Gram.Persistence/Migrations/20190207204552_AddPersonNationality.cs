using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Migrations
{
    public partial class AddPersonNationality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                schema: "Subjects",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Nationality",
                schema: "Subjects",
                table: "Person",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_GeneralType_NationalityId",
                schema: "Subjects",
                table: "Person",
                column: "NationalityId",
                principalSchema: "General",
                principalTable: "GeneralType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_GeneralType_NationalityId",
                schema: "Subjects",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Nationality",
                schema: "Subjects",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                schema: "Subjects",
                table: "Person");
        }
    }
}
