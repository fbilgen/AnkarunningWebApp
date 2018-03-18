using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class TrainingPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "Training");

            migrationBuilder.AddColumn<short>(
                name: "Distance",
                table: "Training",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<long>(
                name: "TrainingPlaceId",
                table: "Training",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Training_TrainingPlaceId",
                table: "Training",
                column: "TrainingPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training",
                column: "TrainingPlaceId",
                principalTable: "TrainingPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training");

            migrationBuilder.DropTable(
                name: "TrainingPlace");

            migrationBuilder.DropIndex(
                name: "IX_Training_TrainingPlaceId",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "TrainingPlaceId",
                table: "Training");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Training",
                nullable: false,
                defaultValue: "");
        }
    }
}
