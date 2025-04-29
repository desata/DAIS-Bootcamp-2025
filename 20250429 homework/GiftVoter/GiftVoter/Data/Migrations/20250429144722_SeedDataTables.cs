using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GiftVoter.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDate", "Name", "Password", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", "hash1", "alice" },
                    { 2, new DateTime(1997, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob Smith", "hash2", "bob" },
                    { 3, new DateTime(1995, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie Davis", "hash3", "charlie" },
                    { 4, new DateTime(2005, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "David Rob", "hash4", "david" },
                    { 5, new DateTime(1998, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom Davis", "hash5", "tom" }
                });

            migrationBuilder.InsertData(
                table: "Gifts",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Coffee Mug" },
                    { 2, "Bluetooth Speaker" },
                    { 3, "Amazon Gift Card" },
                    { 4, "Bike" },
                    { 5, "Car" },
                    { 6, "Fridge" },
                    { 7, "Spoon" }
                });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "BirthdayEmployeeId", "EndTime", "IsActive", "StartTime", "StartedByEmployeeId", "Year" },
                values: new object[] { 1, 2, null, true, new DateTime(2025, 4, 29, 10, 0, 0, 0, DateTimeKind.Utc), 1, 2025 });

            migrationBuilder.InsertData(
                table: "VoteOptions",
                columns: new[] { "Id", "GiftId", "VoteId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 7, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "VoteOptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoteOptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VoteOptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Gifts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
