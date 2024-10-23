using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class ServiceProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletFromId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletFromId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletFromId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletHashFrom",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Wallets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProfileId",
                table: "Wallets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Hash",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Transactions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Error",
                table: "Transactions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ApiKey = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProfiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ServiceProfileId",
                table: "Wallets",
                column: "ServiceProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_ServiceProfiles_ServiceProfileId",
                table: "Wallets",
                column: "ServiceProfileId",
                principalTable: "ServiceProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_ServiceProfiles_ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "ServiceProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "ServiceProfileId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Error",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "Hash",
                table: "Transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletFromId",
                table: "Transactions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalletHashFrom",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletFromId",
                table: "Transactions",
                column: "WalletFromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletFromId",
                table: "Transactions",
                column: "WalletFromId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
