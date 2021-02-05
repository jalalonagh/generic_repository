using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "specialOffer",
                schema: "Shop",
                table: "ShopProducts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "stock",
                schema: "Shop",
                table: "ShopProducts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialOffer",
                schema: "Shop",
                table: "ShopProducts");

            migrationBuilder.DropColumn(
                name: "stock",
                schema: "Shop",
                table: "ShopProducts");
        }
    }
}
