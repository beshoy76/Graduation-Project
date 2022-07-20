using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class mod4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescrptions_Patients_patientId",
                table: "Prescrptions");

            migrationBuilder.RenameColumn(
                name: "patientId",
                table: "Prescrptions",
                newName: "patientid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Prescrptions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Prescrptions",
                newName: "dateTime");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Prescrptions",
                newName: "IdToDelete");

            migrationBuilder.RenameIndex(
                name: "IX_Prescrptions_patientId",
                table: "Prescrptions",
                newName: "IX_Prescrptions_patientid");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescrptions_Patients_patientid",
                table: "Prescrptions",
                column: "patientid",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescrptions_Patients_patientid",
                table: "Prescrptions");

            migrationBuilder.RenameColumn(
                name: "patientid",
                table: "Prescrptions",
                newName: "patientId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prescrptions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "Prescrptions",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "IdToDelete",
                table: "Prescrptions",
                newName: "Phone");

            migrationBuilder.RenameIndex(
                name: "IX_Prescrptions_patientid",
                table: "Prescrptions",
                newName: "IX_Prescrptions_patientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescrptions_Patients_patientId",
                table: "Prescrptions",
                column: "patientId",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
