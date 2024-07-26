using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class RoundModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_Games_GameId1",
                table: "GameRounds");

            migrationBuilder.DropIndex(
                name: "IX_GameRounds_GameId1",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "GameRounds");

            migrationBuilder.AddColumn<decimal>(
                name: "RewardSum",
                table: "Games",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentGameRoundSum",
                table: "GameRounds",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RoundNumber",
                table: "GameRounds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RewardSum",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CurrentGameRoundSum",
                table: "GameRounds");

            migrationBuilder.DropColumn(
                name: "RoundNumber",
                table: "GameRounds");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId1",
                table: "GameRounds",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameId1",
                table: "GameRounds",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_Games_GameId1",
                table: "GameRounds",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
