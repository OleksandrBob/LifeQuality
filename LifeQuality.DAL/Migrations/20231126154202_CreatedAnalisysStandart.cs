using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeQuality.DAL.Migrations
{
    public partial class CreatedAnalisysStandart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AnalysisDate",
                table: "Analyses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AnalysesStandarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnalysisType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysesStandarts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "AnalysisDate",
                table: "Analyses");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Analyses");
        }
    }
}
