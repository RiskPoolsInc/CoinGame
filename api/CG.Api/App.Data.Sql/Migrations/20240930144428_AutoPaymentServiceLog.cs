using System;

using App.Data.Sql.Core;

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Data.Sql.Migrations
{
    public partial class AutoPaymentServiceLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoPaymentServiceLogTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPaymentServiceLogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoPaymentServiceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPaymentServiceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutoPaymentServiceLogs_AutoPaymentServiceLogTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AutoPaymentServiceLogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AutoPaymentServiceLogs_TypeId",
                table: "AutoPaymentServiceLogs",
                column: "TypeId");

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
            
            migrationBuilder.InsertDictionaryItem("AutoPaymentServiceLogTypes",1,"Runned","Runned");
            migrationBuilder.InsertDictionaryItem("AutoPaymentServiceLogTypes",2,"Stopped","Stopped");
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

            migrationBuilder.DropTable(
                name: "AutoPaymentServiceLogs");

            migrationBuilder.DropTable(
                name: "AutoPaymentServiceLogTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId1",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId2",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GameId3",
                table: "Transactions");
        }
    }
}
