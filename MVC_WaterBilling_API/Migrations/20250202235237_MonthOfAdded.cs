using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_WaterBilling_API.Migrations
{
    /// <inheritdoc />
    public partial class MonthOfAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Previous_Reading",
                table: "Meters",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonthOf",
                table: "Meters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthOf",
                table: "Meters");

            migrationBuilder.AlterColumn<double>(
                name: "Previous_Reading",
                table: "Meters",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
