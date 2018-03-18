using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ankarunning.Data.Migrations
{
    public partial class TrainingPalceReq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training");

            migrationBuilder.AlterColumn<long>(
                name: "TrainingPlaceId",
                table: "Training",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training",
                column: "TrainingPlaceId",
                principalTable: "TrainingPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training");

            migrationBuilder.AlterColumn<long>(
                name: "TrainingPlaceId",
                table: "Training",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Training_TrainingPlace_TrainingPlaceId",
                table: "Training",
                column: "TrainingPlaceId",
                principalTable: "TrainingPlace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
