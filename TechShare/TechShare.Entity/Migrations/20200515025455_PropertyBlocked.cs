using Microsoft.EntityFrameworkCore.Migrations;

namespace TechShare.Entity
{
    public partial class PropertyBlocked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Blocked",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blocked",
                table: "AspNetUsers");
        }
    }
}
