using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmsSections_CmsSites_SiteId",
                table: "CmsSections");

            migrationBuilder.DropTable(
                name: "CmsSites");

            migrationBuilder.DropIndex(
                name: "IX_CmsSections_SiteId",
                table: "CmsSections");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "CmsSections");

            migrationBuilder.CreateIndex(
                name: "IX_CmsSections_Name",
                table: "CmsSections",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CmsSections_Name",
                table: "CmsSections");

            migrationBuilder.AddColumn<Guid>(
                name: "SiteId",
                table: "CmsSections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CmsSites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Host = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CmsSites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CmsSections_SiteId",
                table: "CmsSections",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_CmsSites_Name",
                table: "CmsSites",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CmsSections_CmsSites_SiteId",
                table: "CmsSections",
                column: "SiteId",
                principalTable: "CmsSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
