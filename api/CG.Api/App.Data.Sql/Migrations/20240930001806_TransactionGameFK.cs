using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class TransactionGameFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PrivateKey",
                table: "Wallets",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ImportedWalletId",
                table: "Wallets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GameId1",
                table: "Transactions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GameId2",
                table: "Transactions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GameId3",
                table: "Transactions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Games_GameId1",
                table: "Transactions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Games_GameId2",
                table: "Transactions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Games_GameId3",
                table: "Transactions",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Games_GameId1",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Games_GameId2",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Games_GameId3",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId2",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId3",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ImportedWalletId",
                table: "Wallets");

            migrationBuilder.AlterColumn<string>(
                name: "PrivateKey",
                table: "Wallets",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
