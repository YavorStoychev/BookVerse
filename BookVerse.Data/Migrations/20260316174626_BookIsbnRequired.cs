using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookIsbnRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "63fcbb19-434d-4595-802d-8d88d61f9fc8", "AQAAAAIAAYagAAAAEI2FGzxwDYSLxSW5umw7MlSM/G0/mErkaovnGgkQxBNKNcobbhd+VNcP5sfhkC9ciA==", "cdb28fc0-3514-4b56-a539-30970a82665a" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Isbn",
                value: "9780451187949");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Isbn",
                value: "9798432591142");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Isbn",
                value: "9798275420395");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "b1f28f01-0a6f-4ec3-b765-fe3ccc3c396b", "AQAAAAIAAYagAAAAEEMdKv4QZi2OCT3gF2VJ4aZG7Qbcv8WRTS1wc7WbRGhjzUwhmqcrSj1mLJwIYBzMyA==", "490382d7-2428-40c4-a575-3fd0be1ef0ae" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Isbn",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Isbn",
                value: null);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Isbn",
                value: null);
        }
    }
}
