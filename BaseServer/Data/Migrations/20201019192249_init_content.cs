using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class init_content : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
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
                    contentName = table.Column<string>(nullable: false),
                    contentTitle = table.Column<string>(nullable: false),
                    contentSummary = table.Column<string>(nullable: false),
                    contentText = table.Column<string>(nullable: false),
                    language = table.Column<string>(nullable: true),
                    contentParent = table.Column<int>(nullable: true),
                    defaultKeywords = table.Column<string>(nullable: true),
                    defaultTags = table.Column<string>(nullable: true),
                    contentImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents",
                schema: "CMS");
        }
    }
}
