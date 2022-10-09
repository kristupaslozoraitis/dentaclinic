using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentaClinic.Migrations
{
    public partial class RelationsBetweenEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Visits_PatientCardId",
                table: "Visits",
                column: "PatientCardId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitFeedbacks_VisitId",
                table: "VisitFeedbacks",
                column: "VisitId");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitFeedbacks_Visits_VisitId",
                table: "VisitFeedbacks",
                column: "VisitId",
                principalTable: "Visits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_PatientCards_PatientCardId",
                table: "Visits",
                column: "PatientCardId",
                principalTable: "PatientCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitFeedbacks_Visits_VisitId",
                table: "VisitFeedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_PatientCards_PatientCardId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_PatientCardId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_VisitFeedbacks_VisitId",
                table: "VisitFeedbacks");
        }
    }
}
