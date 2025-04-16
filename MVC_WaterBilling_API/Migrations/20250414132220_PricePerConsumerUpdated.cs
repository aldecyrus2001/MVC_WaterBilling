using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_WaterBilling_API.Migrations
{
    /// <inheritdoc />
    public partial class PricePerConsumerUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AmountPerCubicCommercial",
                table: "Settings",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPerCubicCommercial",
                table: "Settings");
        }
    }
}
