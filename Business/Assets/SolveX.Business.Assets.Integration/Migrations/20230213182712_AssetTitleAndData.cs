using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolveX.Business.Assets.Integration.Migrations
{
    /// <inheritdoc />
    public partial class AssetTitleAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Asset",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Asset",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Asset");
        }
    }
}
