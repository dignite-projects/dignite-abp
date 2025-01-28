using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class regionrenametoculture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Regions",
                table: "CmsSites",
                newName: "Cultures");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "CmsEntries",
                newName: "Culture");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Region_Slug",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Culture_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Region_PublishTime_Status",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Culture_PublishTime_Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cultures",
                table: "CmsSites",
                newName: "Regions");

            migrationBuilder.RenameColumn(
                name: "Culture",
                table: "CmsEntries",
                newName: "Region");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Culture_Slug",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Region_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_CmsEntries_SectionId_Culture_PublishTime_Status",
                table: "CmsEntries",
                newName: "IX_CmsEntries_SectionId_Region_PublishTime_Status");
        }
    }
}
