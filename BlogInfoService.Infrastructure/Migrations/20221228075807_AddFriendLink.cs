using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogInfoService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_FriendLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    friendName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    friendUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    headshot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_FriendLink", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_FriendLink");
        }
    }
}
