using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_cart_file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                schema: "Accounting",
                table: "PaymentRecipts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(nullable: true),
                    isActive = table.Column<bool>(nullable: true),
                    creationDateTime = table.Column<DateTime>(nullable: false),
                    creationDay = table.Column<int>(nullable: false),
                    creationPersianDateTime = table.Column<string>(nullable: true),
                    modifiedDateTime = table.Column<DateTime>(nullable: true),
                    modifiedDay = table.Column<int>(nullable: true),
                    modifiedPersianDateTime = table.Column<string>(nullable: true),
                    deletedDateTime = table.Column<DateTime>(nullable: true),
                    deletedDay = table.Column<int>(nullable: true),
                    deletedPersianDateTime = table.Column<string>(nullable: true),
                    priority = table.Column<int>(nullable: true),
                    important = table.Column<int>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    userCreatedId = table.Column<int>(nullable: true),
                    systemTag = table.Column<string>(nullable: true),
                    systemCategory = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false),
                    addressId = table.Column<int>(nullable: true),
                    code = table.Column<string>(nullable: false),
                    barCode = table.Column<string>(nullable: true),
                    qrCode = table.Column<string>(nullable: true),
                    expirationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_UserAddresses_addressId",
                        column: x => x.addressId,
                        principalSchema: "User",
                        principalTable: "UserAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileContents",
                schema: "CMS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(nullable: true),
                    isActive = table.Column<bool>(nullable: true),
                    creationDateTime = table.Column<DateTime>(nullable: false),
                    creationDay = table.Column<int>(nullable: false),
                    creationPersianDateTime = table.Column<string>(nullable: true),
                    modifiedDateTime = table.Column<DateTime>(nullable: true),
                    modifiedDay = table.Column<int>(nullable: true),
                    modifiedPersianDateTime = table.Column<string>(nullable: true),
                    deletedDateTime = table.Column<DateTime>(nullable: true),
                    deletedDay = table.Column<int>(nullable: true),
                    deletedPersianDateTime = table.Column<string>(nullable: true),
                    priority = table.Column<int>(nullable: true),
                    important = table.Column<int>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    userCreatedId = table.Column<int>(nullable: true),
                    systemTag = table.Column<string>(nullable: true),
                    systemCategory = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    fileType = table.Column<string>(nullable: true),
                    size = table.Column<double>(nullable: false),
                    width = table.Column<int>(nullable: false),
                    height = table.Column<int>(nullable: false),
                    path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartAdditionalCosts",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(nullable: true),
                    isActive = table.Column<bool>(nullable: true),
                    creationDateTime = table.Column<DateTime>(nullable: false),
                    creationDay = table.Column<int>(nullable: false),
                    creationPersianDateTime = table.Column<string>(nullable: true),
                    modifiedDateTime = table.Column<DateTime>(nullable: true),
                    modifiedDay = table.Column<int>(nullable: true),
                    modifiedPersianDateTime = table.Column<string>(nullable: true),
                    deletedDateTime = table.Column<DateTime>(nullable: true),
                    deletedDay = table.Column<int>(nullable: true),
                    deletedPersianDateTime = table.Column<string>(nullable: true),
                    priority = table.Column<int>(nullable: true),
                    important = table.Column<int>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    userCreatedId = table.Column<int>(nullable: true),
                    systemTag = table.Column<string>(nullable: true),
                    systemCategory = table.Column<string>(nullable: true),
                    cartId = table.Column<int>(nullable: false),
                    additionalCostId = table.Column<int>(nullable: false),
                    cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartAdditionalCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartAdditionalCosts_AdditionalCosts_additionalCostId",
                        column: x => x.additionalCostId,
                        principalSchema: "Accounting",
                        principalTable: "AdditionalCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartAdditionalCosts_Carts_cartId",
                        column: x => x.cartId,
                        principalSchema: "Accounting",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartProducts",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deleted = table.Column<bool>(nullable: true),
                    isActive = table.Column<bool>(nullable: true),
                    creationDateTime = table.Column<DateTime>(nullable: false),
                    creationDay = table.Column<int>(nullable: false),
                    creationPersianDateTime = table.Column<string>(nullable: true),
                    modifiedDateTime = table.Column<DateTime>(nullable: true),
                    modifiedDay = table.Column<int>(nullable: true),
                    modifiedPersianDateTime = table.Column<string>(nullable: true),
                    deletedDateTime = table.Column<DateTime>(nullable: true),
                    deletedDay = table.Column<int>(nullable: true),
                    deletedPersianDateTime = table.Column<string>(nullable: true),
                    priority = table.Column<int>(nullable: true),
                    important = table.Column<int>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    userCreatedId = table.Column<int>(nullable: true),
                    systemTag = table.Column<string>(nullable: true),
                    systemCategory = table.Column<string>(nullable: true),
                    cartId = table.Column<int>(nullable: false),
                    shopProductId = table.Column<int>(nullable: false),
                    qty = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    totalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProducts_Carts_cartId",
                        column: x => x.cartId,
                        principalSchema: "Accounting",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartProducts_ShopProducts_shopProductId",
                        column: x => x.shopProductId,
                        principalSchema: "Shop",
                        principalTable: "ShopProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecipts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAdditionalCosts_additionalCostId",
                schema: "Accounting",
                table: "CartAdditionalCosts",
                column: "additionalCostId");

            migrationBuilder.CreateIndex(
                name: "IX_CartAdditionalCosts_cartId",
                schema: "Accounting",
                table: "CartAdditionalCosts",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_cartId",
                schema: "Accounting",
                table: "CartProducts",
                column: "cartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_shopProductId",
                schema: "Accounting",
                table: "CartProducts",
                column: "shopProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_addressId",
                schema: "Accounting",
                table: "Carts",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_userId",
                schema: "Accounting",
                table: "Carts",
                column: "userId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRecipts_Carts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts");

            migrationBuilder.DropTable(
                name: "CartAdditionalCosts",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "CartProducts",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "FileContents",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "Accounting");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRecipts_CartId",
                schema: "Accounting",
                table: "PaymentRecipts");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "Accounting",
                table: "PaymentRecipts");
        }
    }
}
