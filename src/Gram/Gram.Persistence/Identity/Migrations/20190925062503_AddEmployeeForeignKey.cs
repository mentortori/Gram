using Microsoft.EntityFrameworkCore.Migrations;

namespace Gram.Persistence.Identity.Migrations
{
    public partial class AddEmployeeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Employee_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Subjects].[Employee] ([Id]);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Employee_EmployeeId];");
        }
    }
}
