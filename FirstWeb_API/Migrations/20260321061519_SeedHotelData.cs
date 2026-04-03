using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstWeb_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedHotelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Hotel",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Created", "Details", "ImageUrl", "Name", "Rate", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury beach resort with ocean view.", "https://images.unsplash.com/photo-1566073771259-6a8506099945", "Ocean Breeze Resort", 1000.0, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Peaceful stay near mountains.", "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa", "Mountain Paradise Lodge", 964.0, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Modern hotel in city center.", "https://images.unsplash.com/photo-1578683010236-d716f9a3f461", "City Grand Hotel", 754.0, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luxury palace style hotel.", "https://images.unsplash.com/photo-1564501049412-61c2a3083791", "Royal Palace Stay", 537.0, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beautiful sunset cliff view rooms.", "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb", "Sunset Cliff Resort", 637.0, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Hotel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
