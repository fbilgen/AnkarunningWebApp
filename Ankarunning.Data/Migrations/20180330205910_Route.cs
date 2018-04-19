using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class Route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training");

            migrationBuilder.DropTable(
                name: "TrainingPlace");

            migrationBuilder.RenameColumn(
                name: "TrainingPlaceId",
                table: "Training",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Training_TrainingPlaceId",
                table: "Training",
                newName: "IX_Training_RouteId");

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Content = table.Column<byte[]>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    Distance = table.Column<short>(nullable: false),
                    FileName = table.Column<string>(nullable: false),
                    Latitude = table.Column<string>(nullable: false),
                    Longitute = table.Column<string>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_Route_RouteId",
                table: "Training");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Training",
                newName: "TrainingPlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Training_RouteId",
                table: "Training",
                newName: "IX_Training_TrainingPlaceId");

            migrationBuilder.CreateTable(
                name: "TrainingPlace",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlace", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training",
                column: "TrainingPlaceId",
                principalTable: "TrainingPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
