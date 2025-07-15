using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BattleCaseBitfinex.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceDifferences",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceDifference = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceDifferences", x => new { x.Timestamp, x.Symbol });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceDifferences");
        }
    }
}
