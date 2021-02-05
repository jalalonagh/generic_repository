using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_update_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ProductFeatures",
                newName: "ProductFeatures",
                newSchema: "Shop");

            migrationBuilder.AddColumn<string>(
                name: "warrenty",
                schema: "Shop",
                table: "ShopProducts",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "value",
                schema: "Shop",
                table: "ProductFeatures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parameter",
                schema: "Shop",
                table: "ProductFeatures",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductComments",
                schema: "Shop",
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
                    productId = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: false),
                    buyRate = table.Column<int>(nullable: false),
                    productRate = table.Column<int>(nullable: false),
                    featureRate = table.Column<int>(nullable: false),
                    technologyRate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_productId",
                        column: x => x.productId,
                        principalSchema: "Shop",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPhotos",
                schema: "Shop",
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
                    productId = table.Column<int>(nullable: false),
                    altText = table.Column<string>(nullable: false),
                    photo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPhotos_Products_productId",
                        column: x => x.productId,
                        principalSchema: "Shop",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopProductColors",
                schema: "Shop",
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
                    shopProductId = table.Column<int>(nullable: false),
                    color = table.Column<string>(nullable: false),
                    colorName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProductColors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopProductColors_ShopProducts_shopProductId",
                        column: x => x.shopProductId,
                        principalSchema: "Shop",
                        principalTable: "ShopProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_productId",
                schema: "Shop",
                table: "ProductComments",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPhotos_productId",
                schema: "Shop",
                table: "ProductPhotos",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProductColors_shopProductId",
                schema: "Shop",
                table: "ShopProductColors",
                column: "shopProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductComments",
                schema: "Shop");

            migrationBuilder.DropTable(
                name: "ProductPhotos",
                schema: "Shop");

            migrationBuilder.DropTable(
                name: "ShopProductColors",
                schema: "Shop");

            migrationBuilder.DropColumn(
                name: "warrenty",
                schema: "Shop",
                table: "ShopProducts");

            migrationBuilder.RenameTable(
                name: "ProductFeatures",
                schema: "Shop",
                newName: "ProductFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "value",
                table: "ProductFeatures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "parameter",
                table: "ProductFeatures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
