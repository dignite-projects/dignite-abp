using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedStorageOfJsonType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_SectionId_CreatorId_PublishTime_Status",
                table: "CmsEntries");

            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_SectionId_Culture_PublishTime_Status",
                table: "CmsEntries");

            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_SectionId_Culture_Slug",
                table: "CmsEntries");

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_CreatorId_SectionId_PublishTime_Status",
                table: "CmsEntries",
                columns: new[] { "CreatorId", "SectionId", "PublishTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_Culture_SectionId_PublishTime_Status",
                table: "CmsEntries",
                columns: new[] { "Culture", "SectionId", "PublishTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_Culture_SectionId_Slug",
                table: "CmsEntries",
                columns: new[] { "Culture", "SectionId", "Slug" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_SectionId",
                table: "CmsEntries",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_CreatorId_SectionId_PublishTime_Status",
                table: "CmsEntries");

            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_Culture_SectionId_PublishTime_Status",
                table: "CmsEntries");

            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_Culture_SectionId_Slug",
                table: "CmsEntries");

            migrationBuilder.DropIndex(
                name: "IX_CmsEntries_SectionId",
                table: "CmsEntries");

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_SectionId_CreatorId_PublishTime_Status",
                table: "CmsEntries",
                columns: new[] { "SectionId", "CreatorId", "PublishTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_SectionId_Culture_PublishTime_Status",
                table: "CmsEntries",
                columns: new[] { "SectionId", "Culture", "PublishTime", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsEntries_SectionId_Culture_Slug",
                table: "CmsEntries",
                columns: new[] { "SectionId", "Culture", "Slug" });
        }
    }
}
