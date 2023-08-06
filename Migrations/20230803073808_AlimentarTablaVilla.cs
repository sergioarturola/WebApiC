using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi1.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImgUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "muchas", "casita chida", new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3613), new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3598), "", 30.0, "Villa del real tecamac", 6, 1234.0 },
                    { 2, "2 pisos", "privada exclusiva", new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3618), new DateTime(2023, 8, 3, 1, 38, 8, 670, DateTimeKind.Local).AddTicks(3617), "", 40.0, "heroes de tecamac", 3, 7890.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
