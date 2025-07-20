using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenge_ATM_API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeedsAndOtherChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Address", "City", "Country", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 1, "Siempre viva 123", "Buenos Aires", "Argentina", new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "BANK_ATM", new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BankId", "CUIT", "CreatedDate", "DNI", "Email", "Name", "Surname", "UpdatedDate" },
                values: new object[] { 1, 1, "00-11222333-4", new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "11222333", "JuanPerez@email.com", "Juan", "Perez", new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Attempts", "Balance", "CreatedDate", "ExpirationDate", "IsLocked", "Number", "PIN", "UpdatedDate", "UserId" },
                values: new object[] { 1, 0, 0.0, new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2030, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "1111111111111111", "AQAAAAIAAYagAAAAEPqQi9cthcz8nRfBaShDpcvD/YipdjrZcdAqjl1XaGDAJQNDdWreHfEhFi5bBg00XA==", new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "Cards");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
