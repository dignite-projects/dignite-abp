using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntryVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revision_IsActive",
                table: "CmsEntries");

            migrationBuilder.DropColumn(
                name: "Revision_Version",
                table: "CmsEntries");

            migrationBuilder.RenameColumn(
                name: "Revision_Notes",
                table: "CmsEntries",
                newName: "VersionNotes");

            migrationBuilder.RenameColumn(
                name: "Revision_InitialId",
                table: "CmsEntries",
                newName: "InitialVersionId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivatedVersion",
                table: "CmsEntries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivatedVersion",
                table: "CmsEntries");

            migrationBuilder.RenameColumn(
                name: "VersionNotes",
                table: "CmsEntries",
                newName: "Revision_Notes");

            migrationBuilder.RenameColumn(
                name: "InitialVersionId",
                table: "CmsEntries",
                newName: "Revision_InitialId");

            migrationBuilder.AddColumn<bool>(
                name: "Revision_IsActive",
                table: "CmsEntries",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Revision_Version",
                table: "CmsEntries",
                type: "int",
                nullable: true);
        }
    }
}
