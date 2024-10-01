using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class RenameServiceProfile_toCreatedetBy_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ServiceProfiles_ServiceProfileId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_ServiceProfiles_ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ServiceProfileId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "ServiceProfileId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Wallets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CreatedById",
                table: "Wallets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatedById",
                table: "Transactions",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ServiceProfiles_CreatedById",
                table: "Transactions",
                column: "CreatedById",
                principalTable: "ServiceProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_ServiceProfiles_CreatedById",
                table: "Wallets",
                column: "CreatedById",
                principalTable: "ServiceProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ServiceProfiles_CreatedById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_ServiceProfiles_CreatedById",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CreatedById",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatedById",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProfileId",
                table: "Wallets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProfileId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ServiceProfileId",
                table: "Wallets",
                column: "ServiceProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ServiceProfileId",
                table: "Transactions",
                column: "ServiceProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ServiceProfiles_ServiceProfileId",
                table: "Transactions",
                column: "ServiceProfileId",
                principalTable: "ServiceProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_ServiceProfiles_ServiceProfileId",
                table: "Wallets",
                column: "ServiceProfileId",
                principalTable: "ServiceProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
