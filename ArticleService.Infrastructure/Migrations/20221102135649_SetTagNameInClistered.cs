using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticleService.Infrastructure.Migrations
{
    public partial class SetTagNameInClistered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "T_ArticleTags",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_T_ArticleTags_TagName",
                table: "T_ArticleTags",
                column: "TagName")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_ArticleTags_TagName",
                table: "T_ArticleTags");

            migrationBuilder.AlterColumn<string>(
                name: "TagName",
                table: "T_ArticleTags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
