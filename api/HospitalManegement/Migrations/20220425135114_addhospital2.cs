using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class addhospital2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Beds");

            migrationBuilder.AddColumn<string>(
                name: "Hospitalsid",
                table: "BookingBeds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingBeds_Hospitalsid",
                table: "BookingBeds",
                column: "Hospitalsid");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingBeds_Hospitals_Hospitalsid",
                table: "BookingBeds",
                column: "Hospitalsid",
                principalTable: "Hospitals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingBeds_Hospitals_Hospitalsid",
                table: "BookingBeds");

            migrationBuilder.DropIndex(
                name: "IX_BookingBeds_Hospitalsid",
                table: "BookingBeds");

            migrationBuilder.DropColumn(
                name: "Hospitalsid",
                table: "BookingBeds");

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
