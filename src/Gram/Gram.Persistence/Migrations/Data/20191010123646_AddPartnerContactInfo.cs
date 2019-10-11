using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Migrations.Data
{
    public partial class AddPartnerContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerContactInfo",
                schema: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerId = table.Column<int>(nullable: false),
                    ContactTypeId = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerContactInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerContactInfo_GeneralType_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalSchema: "General",
                        principalTable: "GeneralType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerContactInfo_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalSchema: "Subjects",
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerContactInfo_ContactType",
                schema: "Subjects",
                table: "PartnerContactInfo",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerContactInfo_Partner",
                schema: "Subjects",
                table: "PartnerContactInfo",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "UQ_PartnerContactInfo_Partner_ContactType",
                schema: "Subjects",
                table: "PartnerContactInfo",
                columns: new[] { "PartnerId", "ContactTypeId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerContactInfo",
                schema: "Subjects");
        }
    }
}
