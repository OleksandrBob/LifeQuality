using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeQuality.DAL.Migrations
{
    public partial class FinishedAnalysisStandart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgeRange",
                table: "AnalysesStandarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "AnalysesStandarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AnalysesStandarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeightRange",
                table: "AnalysesStandarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "AnalysesStandarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LaboratoryName",
                table: "Analyses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeRange",
                table: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "HeightRange",
                table: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AnalysesStandarts");

            migrationBuilder.DropColumn(
                name: "LaboratoryName",
                table: "Analyses");
        }
    }
}
