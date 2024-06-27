using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Data.Sql.Migrations
{
    public partial class UpdatedDictionatyTypesNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameRoundResult_ResultId",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameResult_ResultId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameState_StateId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionState_StateId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "GameResult");

            migrationBuilder.DropTable(
                name: "GameRoundResult");

            migrationBuilder.DropTable(
                name: "GameState");

            migrationBuilder.DropTable(
                name: "TransactionState");

            migrationBuilder.CreateTable(
                name: "GameResultType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResultType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundResultType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundResultType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStateType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStateType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStateType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStateType", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_GameRoundResultType_ResultId",
                table: "GameRounds",
                column: "ResultId",
                principalTable: "GameRoundResultType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameResultType_ResultId",
                table: "Games",
                column: "ResultId",
                principalTable: "GameResultType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStateType_StateId",
                table: "Games",
                column: "StateId",
                principalTable: "GameStateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionStateType_StateId",
                table: "Transactions",
                column: "StateId",
                principalTable: "TransactionStateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameRoundResultType_ResultId",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameResultType_ResultId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStateType_StateId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionStateType_StateId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "GameResultType");

            migrationBuilder.DropTable(
                name: "GameRoundResultType");

            migrationBuilder.DropTable(
                name: "GameStateType");

            migrationBuilder.DropTable(
                name: "TransactionStateType");

            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionState", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_GameRoundResult_ResultId",
                table: "GameRounds",
                column: "ResultId",
                principalTable: "GameRoundResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameResult_ResultId",
                table: "Games",
                column: "ResultId",
                principalTable: "GameResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameState_StateId",
                table: "Games",
                column: "StateId",
                principalTable: "GameState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionState_StateId",
                table: "Transactions",
                column: "StateId",
                principalTable: "TransactionState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
