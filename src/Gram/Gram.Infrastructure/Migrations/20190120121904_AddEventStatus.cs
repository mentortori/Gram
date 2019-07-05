using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Infrastructure.Migrations
{
    public partial class AddEventStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventStatusId",
                schema: "Events",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventStatus",
                schema: "Events",
                table: "Event",
                column: "EventStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_GeneralType_EventStatusId",
                schema: "Events",
                table: "Event",
                column: "EventStatusId",
                principalSchema: "General",
                principalTable: "GeneralType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_GeneralType_EventStatusId",
                schema: "Events",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventStatus",
                schema: "Events",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventStatusId",
                schema: "Events",
                table: "Event");
        }
    }
}
