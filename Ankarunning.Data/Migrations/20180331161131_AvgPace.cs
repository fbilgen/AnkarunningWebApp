using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class AvgPace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Training",
                newName: "AvgPace");

            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "Route",
                newName: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvgPace",
                table: "Training",
                newName: "Distance");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Route",
                newName: "Longitute");
        }
    }
}
