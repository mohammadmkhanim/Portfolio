using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLinkToImageInRecentWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "RecentWorks");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "RecentWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecentWorks_ImageId",
                table: "RecentWorks",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecentWorks_Images_ImageId",
                table: "RecentWorks",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecentWorks_Images_ImageId",
                table: "RecentWorks");

            migrationBuilder.DropIndex(
                name: "IX_RecentWorks_ImageId",
                table: "RecentWorks");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "RecentWorks");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "RecentWorks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
