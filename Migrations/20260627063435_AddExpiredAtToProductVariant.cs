using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowMart.Migrations
{
    /// <inheritdoc />
    public partial class AddExpiredAtToProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "ProductVariants",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "ProductVariants");
        }
    }
}
