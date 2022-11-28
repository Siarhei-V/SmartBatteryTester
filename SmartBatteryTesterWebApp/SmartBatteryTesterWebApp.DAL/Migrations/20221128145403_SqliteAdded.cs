using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBatteryTesterWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SqliteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasurementSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MeasurementName = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementStatus = table.Column<string>(type: "TEXT", nullable: true),
                    DischargeDuration = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    ResultCapacity = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Voltage = table.Column<decimal>(type: "TEXT", nullable: false),
                    Current = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeasurementDateTime = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementSetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_MeasurementSets_MeasurementSetId",
                        column: x => x.MeasurementSetId,
                        principalTable: "MeasurementSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_MeasurementSetId",
                table: "Measurements",
                column: "MeasurementSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "MeasurementSets");
        }
    }
}
