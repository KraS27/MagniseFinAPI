using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagniseFinAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "market_assets",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kind = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    exchange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descriptions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tick_size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_market_assets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mappings",
                columns: table => new
                {
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    exchange = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    defaulrOrderSize = table.Column<int>(type: "int", nullable: false),
                    MarketAssetId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappings", x => new { x.symbol, x.exchange, x.name });
                    table.ForeignKey(
                        name: "FK_mappings_market_assets_MarketAssetId",
                        column: x => x.MarketAssetId,
                        principalTable: "market_assets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_mappings_MarketAssetId",
                table: "mappings",
                column: "MarketAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mappings");

            migrationBuilder.DropTable(
                name: "market_assets");
        }
    }
}
