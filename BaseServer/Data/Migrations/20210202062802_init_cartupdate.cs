using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_cartupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "colorId",
                schema: "Accounting",
                table: "InvoiceProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "colorPrice",
                schema: "Accounting",
                table: "InvoiceProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "warrentyId",
                schema: "Accounting",
                table: "InvoiceProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "warrentyPrice",
                schema: "Accounting",
                table: "InvoiceProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "colorId",
                schema: "Accounting",
                table: "CartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "colorPrice",
                schema: "Accounting",
                table: "CartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "warrentyId",
                schema: "Accounting",
                table: "CartProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "warrentyPrice",
                schema: "Accounting",
                table: "CartProducts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "colorId",
                schema: "Accounting",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "colorPrice",
                schema: "Accounting",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "warrentyId",
                schema: "Accounting",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "warrentyPrice",
                schema: "Accounting",
                table: "InvoiceProducts");

            migrationBuilder.DropColumn(
                name: "colorId",
                schema: "Accounting",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "colorPrice",
                schema: "Accounting",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "warrentyId",
                schema: "Accounting",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "warrentyPrice",
                schema: "Accounting",
                table: "CartProducts");
        }
    }
}
