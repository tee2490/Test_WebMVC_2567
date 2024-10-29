using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp1.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 4, "MicroPhone1", 70.0 },
                    { 2, 9, "MicroPhone2", 83.0 },
                    { 3, 2, "MicroPhone3", 71.0 },
                    { 4, 2, "MicroPhone4", 74.0 },
                    { 5, 2, "MicroPhone5", 28.0 },
                    { 6, 8, "MicroPhone6", 42.0 },
                    { 7, 9, "MicroPhone7", 12.0 },
                    { 8, 6, "MicroPhone8", 85.0 },
                    { 9, 8, "MicroPhone9", 74.0 },
                    { 10, 8, "MicroPhone10", 90.0 },
                    { 11, 8, "MicroPhone11", 61.0 },
                    { 12, 4, "MicroPhone12", 56.0 },
                    { 13, 9, "MicroPhone13", 89.0 },
                    { 14, 8, "MicroPhone14", 67.0 },
                    { 15, 8, "MicroPhone15", 17.0 },
                    { 16, 1, "MicroPhone16", 40.0 },
                    { 17, 8, "MicroPhone17", 43.0 },
                    { 18, 9, "MicroPhone18", 70.0 },
                    { 19, 9, "MicroPhone19", 13.0 },
                    { 20, 7, "MicroPhone20", 20.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
