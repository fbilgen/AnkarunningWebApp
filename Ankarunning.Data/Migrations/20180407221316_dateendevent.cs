using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class dateendevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Event",
                newName: "DateTimeStart");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeEnd",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeEnd",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "DateTimeStart",
                table: "Event",
                newName: "DateTime");
        }
    }
}
