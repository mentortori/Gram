using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Migrations.Data
{
    public partial class AddGuide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guide",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guide_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Subjects",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventGuide",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    GuideId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventGuide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventGuide_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "Events",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventGuide_Guide_GuideId",
                        column: x => x.GuideId,
                        principalSchema: "Subjects",
                        principalTable: "Guide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventGuide_Event",
                schema: "Events",
                table: "EventGuide",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventGuide_Guide",
                schema: "Events",
                table: "EventGuide",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "UQ_EventGuide_Event_Guide",
                schema: "Events",
                table: "EventGuide",
                columns: new[] { "EventId", "GuideId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Guide_Person",
                schema: "Subjects",
                table: "Guide",
                column: "PersonId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventGuide",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Guide",
                schema: "Subjects");
        }
    }
}
