using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Migrations.Data
{
    public partial class ChangedParticipationToAttendee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participation",
                schema: "Events");

            migrationBuilder.CreateTable(
                name: "Attendance",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "Events",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Subjects",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_GeneralType_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_Event",
                schema: "Events",
                table: "Attendance",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_Person",
                schema: "Events",
                table: "Attendance",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_Status",
                schema: "Events",
                table: "Attendance",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "UQ_Attendance_Event_Person",
                schema: "Events",
                table: "Attendance",
                columns: new[] { "EventId", "PersonId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance",
                schema: "Events");

            migrationBuilder.CreateTable(
                name: "Participation",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    StatusDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participation_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "Events",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participation_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Subjects",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Participation_GeneralType_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participation_Event",
                schema: "Events",
                table: "Participation",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_Person",
                schema: "Events",
                table: "Participation",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_Status",
                schema: "Events",
                table: "Participation",
                column: "StatusId");
        }
    }
}
