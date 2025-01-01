using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tables_schema_add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BookStore");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "BookStore");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "BookStore");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Books",
                newSchema: "BookStore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "BookStore",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "BookStore",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Books",
                schema: "BookStore",
                newName: "Books");
        }
    }
}
