using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowMart.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipAndLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_StoreStocks_StockId",
                table: "SaleItems");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "SaleItems",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_StockId",
                table: "SaleItems",
                newName: "IX_SaleItems_VariantId");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "Staffs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Staffs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreStockStockId",
                table: "SaleItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinStock",
                table: "ProductVariants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredPoint = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiredAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Memberships_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_MemberId",
                table: "Sales",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_StoreStockStockId",
                table: "SaleItems",
                column: "StoreStockStockId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_LevelId",
                table: "Memberships",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_ProductVariants_VariantId",
                table: "SaleItems",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_StoreStocks_StoreStockStockId",
                table: "SaleItems",
                column: "StoreStockStockId",
                principalTable: "StoreStocks",
                principalColumn: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Memberships_MemberId",
                table: "Sales",
                column: "MemberId",
                principalTable: "Memberships",
                principalColumn: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_ProductVariants_VariantId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_StoreStocks_StoreStockStockId",
                table: "SaleItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Memberships_MemberId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Sales_MemberId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_StoreStockStockId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "StoreStockStockId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "MinStock",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "SaleItems",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItems_VariantId",
                table: "SaleItems",
                newName: "IX_SaleItems_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_StoreStocks_StockId",
                table: "SaleItems",
                column: "StockId",
                principalTable: "StoreStocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
