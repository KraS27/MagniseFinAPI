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
                name: "mappings",
                columns: table => new
                {
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    exchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    defaulrOrderSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mappings", x => x.name);
                });

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
                name: "mapping_assets",
                columns: table => new
                {
                    MappingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MarketAssetsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mapping_assets", x => new { x.MappingId, x.MarketAssetsId });
                    table.ForeignKey(
                        name: "FK_mapping_assets_mappings_MappingId",
                        column: x => x.MappingId,
                        principalTable: "mappings",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mapping_assets_market_assets_MarketAssetsId",
                        column: x => x.MarketAssetsId,
                        principalTable: "market_assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mapping_assets_MarketAssetsId",
                table: "mapping_assets",
                column: "MarketAssetsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mapping_assets");

            migrationBuilder.DropTable(
                name: "mappings");

            migrationBuilder.DropTable(
                name: "market_assets");
        }
    }
}
