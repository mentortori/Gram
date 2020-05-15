using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Data.Migrations
{
    public partial class AddPartner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partner",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventPartner",
                schema: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    PartnerId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventPartner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventPartner_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "Events",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventPartner_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalSchema: "Subjects",
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventPartner_Event",
                schema: "Events",
                table: "EventPartner",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventPartner_Partner",
                schema: "Events",
                table: "EventPartner",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "UQ_EventPartner_Event_Partner",
                schema: "Events",
                table: "EventPartner",
                columns: new[] { "EventId", "PartnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Partner_Name",
                schema: "Subjects",
                table: "Partner",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventPartner",
                schema: "Events");

            migrationBuilder.DropTable(
                name: "Partner",
                schema: "Subjects");
        }
    }
}
