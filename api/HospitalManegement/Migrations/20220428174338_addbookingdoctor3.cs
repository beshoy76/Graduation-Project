using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class addbookingdoctor3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDoctors_Doctors_doctorid",
                table: "BookingDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDoctors_Patients_patientid",
                table: "BookingDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDoctors",
                table: "BookingDoctors");

            migrationBuilder.DropIndex(
                name: "IX_BookingDoctors_patientid",
                table: "BookingDoctors");

            migrationBuilder.DropColumn(
                name: "id",
                table: "BookingDoctors");

            migrationBuilder.AlterColumn<string>(
                name: "patientid",
                table: "BookingDoctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "doctorid",
                table: "BookingDoctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDoctors",
                table: "BookingDoctors",
                columns: new[] { "patientid", "doctorid" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDoctors_Doctors_doctorid",
                table: "BookingDoctors",
                column: "doctorid",
                principalTable: "Doctors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDoctors_Patients_patientid",
                table: "BookingDoctors",
                column: "patientid",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDoctors_Doctors_doctorid",
                table: "BookingDoctors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingDoctors_Patients_patientid",
                table: "BookingDoctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingDoctors",
                table: "BookingDoctors");

            migrationBuilder.AlterColumn<string>(
                name: "doctorid",
                table: "BookingDoctors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "patientid",
                table: "BookingDoctors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "BookingDoctors",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingDoctors",
                table: "BookingDoctors",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDoctors_patientid",
                table: "BookingDoctors",
                column: "patientid");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDoctors_Doctors_doctorid",
                table: "BookingDoctors",
                column: "doctorid",
                principalTable: "Doctors",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDoctors_Patients_patientid",
                table: "BookingDoctors",
                column: "patientid",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
