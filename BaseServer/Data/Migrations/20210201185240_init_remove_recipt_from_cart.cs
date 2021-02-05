using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_remove_recipt_from_cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRecipts_Carts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRecipts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "Accounting",
                table: "PaymentRecipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                schema: "Accounting",
                table: "PaymentRecipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecipts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRecipts_Carts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts",
                column: "CartId",
                principalSchema: "Accounting",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
