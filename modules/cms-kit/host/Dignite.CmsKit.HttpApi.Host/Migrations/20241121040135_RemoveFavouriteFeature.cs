using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.CmsKit.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFavouriteFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsFavourites");

            migrationBuilder.AddColumn<string>(
                name: "IdempotencyToken",
                table: "CmsComments",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "CmsComments",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CmsUserMarkedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsUserMarkedItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserMarkedItems_TenantId_CreatorId_EntityType_EntityId",
                table: "CmsUserMarkedItems",
                columns: new[] { "TenantId", "CreatorId", "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_CmsUserMarkedItems_TenantId_EntityType_EntityId",
                table: "CmsUserMarkedItems",
                columns: new[] { "TenantId", "EntityType", "EntityId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CmsUserMarkedItems");

            migrationBuilder.DropColumn(
                name: "IdempotencyToken",
                table: "CmsComments");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "CmsComments");

            migrationBuilder.CreateTable(
                name: "CmsFavourites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsFavourites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsFavourites_TenantId_EntityType_EntityId_CreatorId",
                table: "CmsFavourites",
                columns: new[] { "TenantId", "EntityType", "EntityId", "CreatorId" });
        }
    }
}
