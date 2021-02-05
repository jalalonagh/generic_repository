using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_warrenty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Warrenties",
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
                    userId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    logo = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    phone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warrenties", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warrenties",
                schema: "Shop");
        }
    }
}
