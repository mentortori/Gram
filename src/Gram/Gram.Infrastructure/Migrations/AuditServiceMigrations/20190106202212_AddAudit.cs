using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Infrastructure.Migrations.AuditServiceMigrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                schema: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntityState = table.Column<string>(fixedLength: true, maxLength: 1, nullable: true),
                    Entity = table.Column<string>(maxLength: 128, nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    RowModifyUser = table.Column<string>(maxLength: 256, nullable: false),
                    RowModifyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetail",
                schema: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditLogId = table.Column<int>(nullable: false),
                    Property = table.Column<string>(maxLength: 128, nullable: false),
                    OldValue = table.Column<string>(nullable: false),
                    NewValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditDetail_AuditLog_AuditLogId",
                        column: x => x.AuditLogId,
                        principalSchema: "Audit",
                        principalTable: "AuditLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditDetail_AuditLog",
                schema: "Audit",
                table: "AuditDetail",
                column: "AuditLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditDetail",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "AuditLog",
                schema: "Audit");
        }
    }
}
