using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Data.Migrations
{
    public partial class AddPersonContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonContactInfo",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    ContactTypeId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContactInfo_GeneralType_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonContactInfo_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Subjects",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonContactInfo_ContactType",
                schema: "Subjects",
                table: "PersonContactInfo",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContactInfo_Person",
                schema: "Subjects",
                table: "PersonContactInfo",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "UQ_PersonContactInfo_Person_ContactType",
                schema: "Subjects",
                table: "PersonContactInfo",
                columns: new[] { "PersonId", "ContactTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonContactInfo",
                schema: "Subjects");
        }
    }
}
