using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class endtime_orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Orders_OrderId",
                schema: "BookStore",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_OrderId",
                schema: "BookStore",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "BookStore",
                table: "Books");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "BookStore",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Finalized",
                schema: "BookStore",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "BookStore",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Finalized",
                schema: "BookStore",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                schema: "BookStore",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_OrderId",
                schema: "BookStore",
                table: "Books",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Orders_OrderId",
                schema: "BookStore",
                table: "Books",
                column: "OrderId",
                principalSchema: "BookStore",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
