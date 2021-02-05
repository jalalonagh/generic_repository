using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_warrenties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "additionalPrice",
                schema: "Shop",
                table: "ShopProductColors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShopProductWarrenties",
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
                    colorName = table.Column<string>(nullable: false),
                    additionalPrice = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopProductWarrenties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopProductWarrenties_ShopProducts_shopProductId",
                        column: x => x.shopProductId,
                        principalSchema: "Shop",
                        principalTable: "ShopProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopProductWarrenties_shopProductId",
                schema: "Shop",
                table: "ShopProductWarrenties",
                column: "shopProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopProductWarrenties",
                schema: "Shop");

            migrationBuilder.DropColumn(
                name: "additionalPrice",
                schema: "Shop",
                table: "ShopProductColors");
        }
    }
}
