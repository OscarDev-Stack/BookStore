using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class orderbooks_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOrders_Books_BookId",
                schema: "BookStore",
                table: "BookOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_BookOrders_Orders_OrderId",
                schema: "BookStore",
                table: "BookOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOrders",
                schema: "BookStore",
                table: "BookOrders");

            migrationBuilder.DropColumn(
                name: "OderId",
                schema: "BookStore",
                table: "BookOrders");

            migrationBuilder.RenameTable(
                name: "BookOrders",
                schema: "BookStore",
                newName: "OrderBooks",
                newSchema: "BookStore");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrders_OrderId",
                schema: "BookStore",
                table: "OrderBooks",
                newName: "IX_OrderBooks_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrders_BookId",
                schema: "BookStore",
                table: "OrderBooks",
                newName: "IX_OrderBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderBooks",
                schema: "BookStore",
                table: "OrderBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                schema: "BookStore",
                table: "OrderBooks",
                column: "BookId",
                principalSchema: "BookStore",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderBooks_Orders_OrderId",
                schema: "BookStore",
                table: "OrderBooks",
                column: "OrderId",
                principalSchema: "BookStore",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderBooks_Books_BookId",
                schema: "BookStore",
                table: "OrderBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderBooks_Orders_OrderId",
                schema: "BookStore",
                table: "OrderBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderBooks",
                schema: "BookStore",
                table: "OrderBooks");

            migrationBuilder.RenameTable(
                name: "OrderBooks",
                schema: "BookStore",
                newName: "BookOrders",
                newSchema: "BookStore");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBooks_OrderId",
                schema: "BookStore",
                table: "BookOrders",
                newName: "IX_BookOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderBooks_BookId",
                schema: "BookStore",
                table: "BookOrders",
                newName: "IX_BookOrders_BookId");

            migrationBuilder.AddColumn<int>(
                name: "OderId",
                schema: "BookStore",
                table: "BookOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOrders",
                schema: "BookStore",
                table: "BookOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrders_Books_BookId",
                schema: "BookStore",
                table: "BookOrders",
                column: "BookId",
                principalSchema: "BookStore",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrders_Orders_OrderId",
                schema: "BookStore",
                table: "BookOrders",
                column: "OrderId",
                principalSchema: "BookStore",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
