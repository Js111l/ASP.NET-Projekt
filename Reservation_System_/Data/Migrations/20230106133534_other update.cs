using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation_System_.Data.Migrations
{
    public partial class otherupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_roomType",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_roomType",
                table: "Room");
        }
    }
}
