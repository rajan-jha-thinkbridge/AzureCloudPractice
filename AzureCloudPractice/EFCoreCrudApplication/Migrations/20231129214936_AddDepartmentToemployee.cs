using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreCrudApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentToemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "employees");
        }
    }
}
