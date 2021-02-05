using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_warrenties_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "color",
                schema: "Shop",
                table: "ShopProductWarrenties");

            migrationBuilder.DropColumn(
                name: "colorName",
                schema: "Shop",
                table: "ShopProductWarrenties");

            migrationBuilder.AddColumn<int>(
                name: "month",
                schema: "Shop",
                table: "ShopProductWarrenties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "warrentyName",
                schema: "Shop",
                table: "ShopProductWarrenties",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "month",
                schema: "Shop",
                table: "ShopProductWarrenties");

            migrationBuilder.DropColumn(
                name: "warrentyName",
                schema: "Shop",
                table: "ShopProductWarrenties");

            migrationBuilder.AddColumn<string>(
                name: "color",
                schema: "Shop",
                table: "ShopProductWarrenties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "colorName",
                schema: "Shop",
                table: "ShopProductWarrenties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
