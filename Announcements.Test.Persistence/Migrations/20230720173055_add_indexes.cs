using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Announcements.Test.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedAt",
                table: "Announcements",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_ExpirationDate",
                table: "Announcements",
                column: "ExpirationDate");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Rating",
                table: "Announcements",
                column: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatedAt",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_ExpirationDate",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_Rating",
                table: "Announcements");
        }
    }
}
