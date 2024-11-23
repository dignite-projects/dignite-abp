using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.CmsKit.Migrations
{
    /// <inheritdoc />
    public partial class DeviceInfoFieldAddedToVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CmsVisits_TenantId_EntityType_EntityId_ClientIpAddress",
                table: "CmsVisits");

            migrationBuilder.DropIndex(
                name: "IX_CmsVisits_TenantId_EntityType_EntityId_CreatorId",
                table: "CmsVisits");

            migrationBuilder.DropColumn(
                name: "UserAgent",
                table: "CmsVisits");

            migrationBuilder.AlterColumn<string>(
                name: "ClientIpAddress",
                table: "CmsVisits",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "BrowserInfo",
                table: "CmsVisits",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeviceInfo",
                table: "CmsVisits",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CmsVisits_TenantId_EntityType_CreatorId_EntityId_CreationTime",
                table: "CmsVisits",
                columns: new[] { "TenantId", "EntityType", "CreatorId", "EntityId", "CreationTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CmsVisits_TenantId_EntityType_CreatorId_EntityId_CreationTime",
                table: "CmsVisits");

            migrationBuilder.DropColumn(
                name: "BrowserInfo",
                table: "CmsVisits");

            migrationBuilder.DropColumn(
                name: "DeviceInfo",
                table: "CmsVisits");

            migrationBuilder.AlterColumn<string>(
                name: "ClientIpAddress",
                table: "CmsVisits",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "UserAgent",
                table: "CmsVisits",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CmsVisits_TenantId_EntityType_EntityId_ClientIpAddress",
                table: "CmsVisits",
                columns: new[] { "TenantId", "EntityType", "EntityId", "ClientIpAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsVisits_TenantId_EntityType_EntityId_CreatorId",
                table: "CmsVisits",
                columns: new[] { "TenantId", "EntityType", "EntityId", "CreatorId" });
        }
    }
}
