using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Data.Migrations
{
    public partial class CreateInitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Subjects");

            migrationBuilder.EnsureSchema(
                name: "Events");

            migrationBuilder.EnsureSchema(
                name: "General");

            migrationBuilder.CreateTable(
                name: "GeneralType",
                schema: "General",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    IsListed = table.Column<bool>(nullable: false, defaultValue: true),
                    IsFixed = table.Column<bool>(nullable: false, defaultValue: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneralType_GeneralType_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(maxLength: 50, nullable: false),
                    EventStatusId = table.Column<int>(nullable: false),
                    EventDescription = table.Column<string>(maxLength: 4000, nullable: false),
                    EventDate = table.Column<DateTime>(type: "date", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_GeneralType_EventStatusId",
                        column: x => x.EventStatusId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    NationalityId = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_GeneralType_NationalityId",
                        column: x => x.NationalityId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
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

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    DateOfEmployment = table.Column<DateTime>(type: "date", nullable: true),
                    EmploymentExpiryDate = table.Column<DateTime>(type: "date", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Subjects",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventStatus",
                schema: "Events",
                table: "Event",
                column: "EventStatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_GeneralType_Parent",
                schema: "General",
                table: "GeneralType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UQ_GeneralType_Title_Parent",
                schema: "General",
                table: "GeneralType",
                columns: new[] { "Title", "ParentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Person",
                schema: "Subjects",
                table: "Employee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_Nationality",
                schema: "Subjects",
                table: "Person",
                column: "NationalityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participation",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Subjects");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Subjects");

            migrationBuilder.DropTable(
                name: "GeneralType",
                schema: "General");
        }
    }
}
