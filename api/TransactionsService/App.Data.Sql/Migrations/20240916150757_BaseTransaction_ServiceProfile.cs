using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class BaseTransaction_ServiceProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceProfileId",
                table: "Transactions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ServiceProfiles_ServiceProfileId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ServiceProfileId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ServiceProfileId",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
