using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableBookIsbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Books",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Books");

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "df1c3a0f-1234-4cde-bb55-d5f15a6aabcd",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            //    values: new object[] { "10538405-d80a-4a31-a600-2cc5b8e839bb", "AQAAAAIAAYagAAAAEEskfpky74aL26cPx8KKCzm33CrypjlP2ZUwJbjhw7SkUqa0pcJSXw8VNMLJnlxYJw==", "bff6a46a-add8-4efd-96dc-bf187e8896cd" });
        }
    }
}
