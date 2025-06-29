using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class newWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing primary key (composite key)
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists");

            // Drop the existing Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "WishLists");

            // Add a new Id column with identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WishLists",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Set new Id column as the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists",
                column: "Id");

            // Optional: Add index on AppUserId for performance
            migrationBuilder.CreateIndex(
                name: "IX_WishLists_AppUserId",
                table: "WishLists",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the new primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists");

            // Drop index
            migrationBuilder.DropIndex(
                name: "IX_WishLists_AppUserId",
                table: "WishLists");

            // Drop new Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "WishLists");

            // Recreate the old Id column without identity
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "WishLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Re-add composite key as before
            migrationBuilder.AddPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists",
                columns: new[] { "AppUserId", "HotelId" });
        }
    }
}
