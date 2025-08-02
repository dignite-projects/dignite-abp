using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations
{
    /// <inheritdoc />
    public partial class RenameLocalToLocale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Local",
                table: "pbl_Posts",
                newName: "Locale");

            migrationBuilder.RenameColumn(
                name: "Local",
                table: "pbl_Categories",
                newName: "Locale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "pbl_Posts",
                newName: "Local");

            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "pbl_Categories",
                newName: "Local");
        }
    }
}
