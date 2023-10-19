using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Abp.UserPoints.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpUserPointsItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointsDefinitionName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PointsWorkflowName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PointsType = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserPointsItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserPointsOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Redeems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessOrderType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BusinessOrderNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserPointsOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserPointsBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserPointsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserPointsBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserPointsBlocks_AbpUserPointsItems_UserPointsItemId",
                        column: x => x.UserPointsItemId,
                        principalTable: "AbpUserPointsItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserPointsBlocks_UserPointsItemId",
                table: "AbpUserPointsBlocks",
                column: "UserPointsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserPointsItems_UserId_PointsType_PointsDefinitionName_PointsWorkflowName_CreationTime_ExpirationDate",
                table: "AbpUserPointsItems",
                columns: new[] { "UserId", "PointsType", "PointsDefinitionName", "PointsWorkflowName", "CreationTime", "ExpirationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserPointsOrders_BusinessOrderType_BusinessOrderNumber",
                table: "AbpUserPointsOrders",
                columns: new[] { "BusinessOrderType", "BusinessOrderNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserPointsOrders_UserId_CreationTime",
                table: "AbpUserPointsOrders",
                columns: new[] { "UserId", "CreationTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpUserPointsBlocks");

            migrationBuilder.DropTable(
                name: "AbpUserPointsOrders");

            migrationBuilder.DropTable(
                name: "AbpUserPointsItems");
        }
    }
}
