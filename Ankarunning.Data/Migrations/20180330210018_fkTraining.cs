using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class fkTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

         migrationBuilder.AddForeignKey(
             name: "FK_Training_Route_RouteId",
             table: "Training",
             column: "RouteId",
             principalTable: "Route",
             principalColumn: "Id",
             onDelete: ReferentialAction.Cascade);
      }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
