using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_update_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceProducts",
                newName: "InvoiceProducts",
                newSchema: "Accounting");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "InvoiceProducts",
                schema: "Accounting",
                newName: "InvoiceProducts");
        }
    }
}
