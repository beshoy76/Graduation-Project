using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class addhospital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hospitalsid",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hospitalsid",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hospitalsid",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hospitalsid",
                table: "Beds",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hospitalid",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Hospitalsid",
                table: "Patients",
                column: "Hospitalsid");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Hospitalsid",
                table: "Doctors",
                column: "Hospitalsid");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Hospitalsid",
                table: "Departments",
                column: "Hospitalsid");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_Hospitalsid",
                table: "Beds",
                column: "Hospitalsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Beds_Hospitals_Hospitalsid",
                table: "Beds",
                column: "Hospitalsid",
                principalTable: "Hospitals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Hospitals_Hospitalsid",
                table: "Departments",
                column: "Hospitalsid",
                principalTable: "Hospitals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Hospitals_Hospitalsid",
                table: "Doctors",
                column: "Hospitalsid",
                principalTable: "Hospitals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_Hospitalsid",
                table: "Patients",
                column: "Hospitalsid",
                principalTable: "Hospitals",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beds_Hospitals_Hospitalsid",
                table: "Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Hospitals_Hospitalsid",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Hospitals_Hospitalsid",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_Hospitalsid",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Hospitalsid",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Hospitalsid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Hospitalsid",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Beds_Hospitalsid",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "Hospitalsid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Hospitalsid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Hospitalsid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Hospitalsid",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "hospitalid",
                table: "Beds");
        }
    }
}
