using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBatteryTesterWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DischargeDurationAndResultCapacityToMeasurementSetAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DischargeDuration",
                table: "MeasurementSets",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ResultCapacity",
                table: "MeasurementSets",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DischargeDuration",
                table: "MeasurementSets");

            migrationBuilder.DropColumn(
                name: "ResultCapacity",
                table: "MeasurementSets");
        }
    }
}
