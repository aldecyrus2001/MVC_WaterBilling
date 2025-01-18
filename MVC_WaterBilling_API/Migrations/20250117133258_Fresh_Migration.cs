using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_WaterBilling_API.Migrations
{
    /// <inheritdoc />
    public partial class Fresh_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.AlterColumn<double>(
                name: "PenaltyAmount",
                table: "Settings",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "AmountPerCubic",
                table: "Settings",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "PenaltyIncluded",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Reading_Date",
                table: "Meters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUsed",
                table: "Advances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_SettingID",
                table: "Settings",
                column: "SettingID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Settings_SettingID",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Reading_Date",
                table: "Meters");

            migrationBuilder.AlterColumn<double>(
                name: "PenaltyAmount",
                table: "Settings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AmountPerCubic",
                table: "Settings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PenaltyIncluded",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUsed",
                table: "Advances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    PenaltiesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateImplemented = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PenaltyAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.PenaltiesID);
                });
        }
    }
}
