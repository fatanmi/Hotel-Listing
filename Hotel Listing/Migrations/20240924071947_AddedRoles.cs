using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel_Listing.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f74f152-a742-4aeb-b687-52af903a9bf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ca08db0-64f6-40a8-b1c9-edcc6b4ecfca");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cada693-69af-4fdc-a328-13c0fefdb70b", null, "Admin", "ADMIN" },
                    { "75904ca2-6814-4749-9981-5b96d56ca5ad", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cada693-69af-4fdc-a328-13c0fefdb70b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75904ca2-6814-4749-9981-5b96d56ca5ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f74f152-a742-4aeb-b687-52af903a9bf8", null, "User", "USER" },
                    { "9ca08db0-64f6-40a8-b1c9-edcc6b4ecfca", null, "Admin", "ADMIN" }
                });
        }
    }
}
