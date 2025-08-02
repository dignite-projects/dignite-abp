using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations
{
    /// <inheritdoc />
    public partial class AddFileExplorerModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FepDirectoryDescriptors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FepDirectoryDescriptors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FepFileDescriptors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DirectoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BlobName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Md5 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ReferBlobName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FepFileDescriptors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FepDirectoryDescriptors_TenantId_ContainerName_CreatorId_ParentId",
                table: "FepDirectoryDescriptors",
                columns: new[] { "TenantId", "ContainerName", "CreatorId", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "IX_FepFileDescriptors_TenantId_ContainerName_BlobName",
                table: "FepFileDescriptors",
                columns: new[] { "TenantId", "ContainerName", "BlobName" });

            migrationBuilder.CreateIndex(
                name: "IX_FepFileDescriptors_TenantId_ContainerName_CreationTime_CreatorId_DirectoryId",
                table: "FepFileDescriptors",
                columns: new[] { "TenantId", "ContainerName", "CreationTime", "CreatorId", "DirectoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FepDirectoryDescriptors");

            migrationBuilder.DropTable(
                name: "FepFileDescriptors");
        }
    }
}
