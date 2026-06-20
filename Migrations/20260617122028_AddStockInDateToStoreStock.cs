using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowMart.Migrations
{
    /// <inheritdoc />
    public partial class AddStockInDateToStoreStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "StockInDate",
                table: "StoreStocks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockInDate",
                table: "StoreStocks");
        }
    }
}
