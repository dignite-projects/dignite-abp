using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dignite.Cms.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFieldAndEntryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CmsFields");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CmsFields");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CmsFieldGroups");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CmsFieldGroups");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "CmsEntryTypes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "CmsEntryTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CmsFields",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CmsFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CmsFieldGroups",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CmsFieldGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "CmsEntryTypes",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "CmsEntryTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
