using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shendeti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class asf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

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
                    { "92684e88-d354-4acd-89b9-dcfcb4af6a76", null, "Patient", "PATIENT" },
                    { "a9389e3d-bf6b-4f70-9c79-5b8a63025a5b", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b50f60d7-790a-45dc-9754-643b74aa5338", "AQAAAAIAAYagAAAAENAFRs79WlgjKU4UYpQort/LTXsRPuEtPGsp2o58LDWnUJrAbidnumub4HngOJza2Q==", "8f5c5ffe-0e0b-4788-a128-036c13f21c47" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92684e88-d354-4acd-89b9-dcfcb4af6a76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9389e3d-bf6b-4f70-9c79-5b8a63025a5b");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cities_CityId",
                table: "AspNetUsers",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
