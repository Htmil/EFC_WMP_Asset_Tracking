using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFC_WMP_Asset_Tracking.Migrations
{
    /// <inheritdoc />
    public partial class AddedSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Computers",
                columns: new[] { "Id", "Brand", "Currency", "Model", "Office", "PriceUSD", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 1, "ASUS ROG", "SEK", "B550-F", "Sweden", 243m, new DateOnly(2020, 11, 24), "Computer" },
                    { 2, "HP", "USD", "14S-FQ1010NO", "USA", 679m, new DateOnly(2022, 1, 30), "Computer" },
                    { 3, "HP", "EUR", "Elitebook", "Greece", 2234m, new DateOnly(2021, 8, 30), "Computer" },
                    { 4, "HP", "SEK", "Elitebook", "Sweden", 2234m, new DateOnly(2020, 7, 30), "Computer" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Brand", "Currency", "Model", "Office", "PriceUSD", "PurchaseDate", "Type" },
                values: new object[,]
                {
                    { 5, "Samsung", "SEK", "S20 Plus", "Sweden", 1500m, new DateOnly(2020, 9, 12), "Phone" },
                    { 6, "Sony Xperia", "USD", "10 III", "USA", 800m, new DateOnly(2020, 3, 6), "Phone" },
                    { 7, "Iphone", "EUR", "10", "Greece", 951m, new DateOnly(2018, 11, 25), "Phone" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Computers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Phones",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
