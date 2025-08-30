using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shendeti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class afs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "153a4f0f-f74f-45c2-b925-74749eedd1d5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e57e12d3-6726-4b19-a48c-06ba07c2ae8a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49010798-27d6-4eec-a043-66f23a00133c", null, "Patient", "PATIENT" },
                    { "cb98b1a9-0656-4a0b-bb52-4891e5620a92", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a48ff29c-5a09-4e95-a41c-d5adaebd8e6e", "AQAAAAIAAYagAAAAEIcc/Q3YVFHwFZZ2lKhd11+7S32N4YedyPi2lQsWBD65IjzqOpe0xLBheL4+csfxwg==", "3c046ec0-2e55-4ef3-aa7d-d826016e1771" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49010798-27d6-4eec-a043-66f23a00133c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb98b1a9-0656-4a0b-bb52-4891e5620a92");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "153a4f0f-f74f-45c2-b925-74749eedd1d5", null, "Doctor", "DOCTOR" },
                    { "e57e12d3-6726-4b19-a48c-06ba07c2ae8a", null, "Patient", "PATIENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4872d6d-ceaf-4c58-9c9a-a232b91dfb37", "AQAAAAIAAYagAAAAEN5Vovhljj0mxd99ndVIzdcz6Q3MTujSG8ND7bSUNINO1ffDUuz7AB7OXrjClq0GiQ==", "54c5f437-03e8-4640-9675-954b723bd694" });
        }
    }
}
