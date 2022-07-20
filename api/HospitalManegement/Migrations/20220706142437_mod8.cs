using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalManegement.Migrations
{
    public partial class mod8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "medicineName",
                table: "Prescrptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "medicineName",
                table: "Prescrptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
