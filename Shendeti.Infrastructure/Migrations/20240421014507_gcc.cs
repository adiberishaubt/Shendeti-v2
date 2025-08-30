using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shendeti.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d20c091-0d59-4932-b490-dd7f705c6d54");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "91ab75e3-872a-4ecd-ad1a-8326f9b8153c");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

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
                    { "1d20c091-0d59-4932-b490-dd7f705c6d54", null, "Doctor", "DOCTOR" },
                    { "91ab75e3-872a-4ecd-ad1a-8326f9b8153c", null, "Patient", "PATIENT" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1139feff-b3a0-4943-a705-02f3021f19e9", "AQAAAAIAAYagAAAAEBeap9Mjp+uEaCxKoIHaHfAIIFecSAMCkpTrIK7dRQcJPgzvBVrSdeGNsAzzA1E/OQ==", "ab6bd822-1242-451d-9b1b-532e1cd2227b" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
