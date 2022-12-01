using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentaClinic.Migrations
{
    public partial class Addedcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorFullName",
                table: "FreeVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorFullName",
                table: "FreeVisits");
        }
    }
}
