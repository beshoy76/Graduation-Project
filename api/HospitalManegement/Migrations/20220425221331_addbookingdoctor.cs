using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class addbookingdoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingDoctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doctorid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    patientid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    reservedate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDoctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_BookingDoctors_Doctors_doctorid",
                        column: x => x.doctorid,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDoctors_Patients_patientid",
                        column: x => x.patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDoctors_doctorid",
                table: "BookingDoctors",
                column: "doctorid");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDoctors_patientid",
                table: "BookingDoctors",
                column: "patientid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDoctors");
        }
    }
}
