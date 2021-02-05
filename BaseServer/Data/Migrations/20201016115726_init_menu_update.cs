using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_menu_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "customClassStyleNames",
                schema: "Base",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "menu",
                schema: "Base",
                table: "Menus",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parent",
                schema: "Base",
                table: "Menus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customClassStyleNames",
                schema: "Base",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "menu",
                schema: "Base",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "parent",
                schema: "Base",
                table: "Menus");
        }
    }
}
