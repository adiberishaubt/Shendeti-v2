using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shendeti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GDA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services");

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
                    { "37b7389c-9cd1-4afb-b5f6-da534b90dfc3", null, "Patient", "PATIENT" },
                    { "deb50d7a-2b01-456d-b6bc-fdedaaf4e8be", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123admin",
                columns: new[] { "BloodType", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { 1, "3284ac9c-2d1b-4f75-a67c-687d6aeeb323", "AQAAAAIAAYagAAAAEMpb5wYrMYRH4aSRF1jUe6z1A++jaDyG8VF+xt0zUCKBIcJWTqOe/ORJhb8TXbNUYw==", "e6488b24-39b2-48e6-975c-0e8cb43bf2d3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b7389c-9cd1-4afb-b5f6-da534b90dfc3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "deb50d7a-2b01-456d-b6bc-fdedaaf4e8be");

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
                columns: new[] { "BloodType", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "b50f60d7-790a-45dc-9754-643b74aa5338", "AQAAAAIAAYagAAAAENAFRs79WlgjKU4UYpQort/LTXsRPuEtPGsp2o58LDWnUJrAbidnumub4HngOJza2Q==", "8f5c5ffe-0e0b-4788-a128-036c13f21c47" });

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Specializations_SpecializationId",
                table: "Services",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id");
        }
    }
}
