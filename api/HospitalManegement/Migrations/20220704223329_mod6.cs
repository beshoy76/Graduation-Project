using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class mod6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Medicines");

            migrationBuilder.RenameColumn(
                name: "medicineId",
                table: "Medicines",
                newName: "MedicineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicineId",
                table: "Medicines",
                newName: "medicineId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
