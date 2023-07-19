using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Announcements.EF.Migrations
{
    /// <inheritdoc />
    public partial class add_file : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Announcements");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image_Data",
                table: "Announcements",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Image_Extension",
                table: "Announcements",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image_Name",
                table: "Announcements",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Number",
                table: "Announcements",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Announcements_Number",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Image_Data",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Image_Extension",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Image_Name",
                table: "Announcements");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Announcements",
                type: "bytea",
                maxLength: 4194304,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
