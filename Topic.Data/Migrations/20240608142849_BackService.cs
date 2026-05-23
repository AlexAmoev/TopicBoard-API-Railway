using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Topic.Data.Migrations
{
    /// <inheritdoc />
    public partial class BackService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8716071C-1D9B-48FD-B3D0-F059C4FB8031",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "756f74cf-f399-4315-8117-a30d964a913b", "AQAAAAIAAYagAAAAEBo4ii+pOHWR5TAb6OdeNOm2ZGrRkZ24mHCBjMfWy6wQVT+UjxDi5hzHrLiOHD5bhg==", "46309d45-56c9-42db-8ab3-459ebc07b468" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "87746F88-DC38-4756-924A-B95CFF3A1D8A",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4de48351-545f-4518-a9f3-7eb01061eb3a", "AQAAAAIAAYagAAAAEGWC+GVA+oK81eB5wIQhVggIwPiiFhJcAs1xVjSn735FoYt3EYIuQmpBZhtGmYWpqw==", "eb9d9119-2d73-4f29-a8d3-c4c551e2c931" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D514EDC9-94BB-416F-AF9D-7C13669689C9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6bfad177-390d-4f9b-9c53-2ecc4320bbc8", "AQAAAAIAAYagAAAAEFUm4ssUQytRIQ27qYe40EdQmCyD8eK7WSDszpChnqZ/ofhml/p6ld1codoadllPhw==", "93b47a9f-38d7-44e2-b1a6-1766960bfcfc" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PostedDate",
                value: new DateTime(2024, 6, 8, 18, 28, 49, 25, DateTimeKind.Local).AddTicks(8246));

            migrationBuilder.UpdateData(
                table: "Topics",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartDate",
                value: new DateTime(2024, 6, 8, 18, 28, 49, 25, DateTimeKind.Local).AddTicks(8100));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
