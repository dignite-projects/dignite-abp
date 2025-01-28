using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class languagerenametoregion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Languages",
                table: "CmsSites",
                newName: "Regions");

            migrationBuilder.RenameColumn(
                name: "Host",
                table: "CmsSites",
                newName: "HostUrl");

            migrationBuilder.RenameColumn(
                name: "Language",
                table: "CmsEntries",
                newName: "Region");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Language_Slug",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Region_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Language_PublishTime_Status",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Region_PublishTime_Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Regions",
                table: "CmsSites",
                newName: "Languages");

            migrationBuilder.RenameColumn(
                name: "HostUrl",
                table: "CmsSites",
                newName: "Host");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "CmsEntries",
                newName: "Language");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Region_Slug",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Language_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Region_PublishTime_Status",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Language_PublishTime_Status");
        }
    }
}
