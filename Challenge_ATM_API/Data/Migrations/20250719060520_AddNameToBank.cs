using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge_ATM_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Banks");
        }
    }
}
