using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentaClinic.Migrations
{
    public partial class Minorchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeVisit_AspNetUsers_UserId",
                table: "FreeVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_FreeVisit_Service_ServiceId",
                table: "FreeVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_AspNetUsers_UserId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_FreeVisit_FreeVisitId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeVisit",
                table: "FreeVisit");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "FreeVisit",
                newName: "FreeVisits");

            migrationBuilder.RenameIndex(
                name: "IX_Service_UserId",
                table: "Services",
                newName: "IX_Services_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FreeVisit_UserId",
                table: "FreeVisits",
                newName: "IX_FreeVisits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FreeVisit_ServiceId",
                table: "FreeVisits",
                newName: "IX_FreeVisits_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeVisits",
                table: "FreeVisits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVisits_AspNetUsers_UserId",
                table: "FreeVisits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVisits_Services_ServiceId",
                table: "FreeVisits",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_FreeVisits_FreeVisitId",
                table: "Visits",
                column: "FreeVisitId",
                principalTable: "FreeVisits",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreeVisits_AspNetUsers_UserId",
                table: "FreeVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_FreeVisits_Services_ServiceId",
                table: "FreeVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_UserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_FreeVisits_FreeVisitId",
                table: "Visits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FreeVisits",
                table: "FreeVisits");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "FreeVisits",
                newName: "FreeVisit");

            migrationBuilder.RenameIndex(
                name: "IX_Services_UserId",
                table: "Service",
                newName: "IX_Service_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FreeVisits_UserId",
                table: "FreeVisit",
                newName: "IX_FreeVisit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FreeVisits_ServiceId",
                table: "FreeVisit",
                newName: "IX_FreeVisit_ServiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FreeVisit",
                table: "FreeVisit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVisit_AspNetUsers_UserId",
                table: "FreeVisit",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FreeVisit_Service_ServiceId",
                table: "FreeVisit",
                column: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_AspNetUsers_UserId",
                table: "Service",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_FreeVisit_FreeVisitId",
                table: "Visits",
                column: "FreeVisitId",
                principalTable: "FreeVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
