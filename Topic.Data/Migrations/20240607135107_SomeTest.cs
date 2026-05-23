using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Topic.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c6932077-2f6c-4934-a6dd-926354fe819c", "AQAAAAIAAYagAAAAEKgOUUdpVwGVGTJhbYJVvKjjiGi5Nit5krdMMASvB815bBohtzhp66S4y90qxPhFTQ==", "be3d2419-f07e-46c3-9d6e-bfbf3bb56801" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "accd1eb8-355d-4ae2-a15e-d0ca4b99a4df", "AQAAAAIAAYagAAAAEM+V7YO4sHSPMCshtnZKY0m26eYKlInGbmf78QVAs3UheSJc3J4HS20lx+e71UhLqg==", "675aacc9-5a7d-46c5-b912-e742c5fffa1e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0da55d22-787b-44f4-9898-28e6eec9ae06", "AQAAAAIAAYagAAAAEGApdzfQIcRmCs5VWDLXX6lPj3as8ZrcSZ+6UZMblZhQ5BZjWtm1AfCPBMeGnoP0jg==", "9222d3de-6cc1-45c3-a0be-17976d9ee3af" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedDate",
                value: new DateTime(2024, 6, 7, 17, 51, 7, 162, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 6, 7, 17, 51, 7, 162, DateTimeKind.Local).AddTicks(7314));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54ad9832-9e41-4469-8fcb-a6f3519ee321", "AQAAAAIAAYagAAAAEAf2GXjdxaMOJulDqGrhr9Rgvw5kCPD9xv71wniMi9NkAlB59MZ6uTsTOXuYC3tc/Q==", "24c21dd5-d470-48d0-bcb4-001f51a151c7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af6cf69a-d5b1-4c29-b0e8-1efc5e930ee1", "AQAAAAIAAYagAAAAEINsIWSh94MYDq7OEbxMi/q2WX0v8RECLTpGt1qSJixYeGvZ2+sO5qfnOrYDKfg05Q==", "1a811f29-526d-49d5-83af-cc1944f52820" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c87dcda7-879b-4d9c-9424-8702e5c0cb69", "AQAAAAIAAYagAAAAEFRu1sgyTfVzW6LGysbwTaWO3LK/kTMUda3aHHUcRJP5veH3SE3Zn2Jf57D/+LRMYQ==", "82e386e9-01bb-44dd-a646-6c1d32c6babc" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedDate",
                value: new DateTime(2024, 6, 6, 17, 52, 8, 215, DateTimeKind.Local).AddTicks(3198));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 6, 6, 17, 52, 8, 215, DateTimeKind.Local).AddTicks(3067));
        }
    }
}
