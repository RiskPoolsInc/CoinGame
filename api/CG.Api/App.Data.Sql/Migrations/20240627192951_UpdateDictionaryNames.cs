using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class UpdateDictionaryNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameRoundResultType_ResultId",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameResultType_ResultId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionStateType_StateId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionType_TypeId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletType_TypeId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletType",
                table: "WalletType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionType",
                table: "TransactionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionStateType",
                table: "TransactionStateType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRoundResultType",
                table: "GameRoundResultType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameResultType",
                table: "GameResultType");

            migrationBuilder.RenameTable(
                name: "WalletType",
                newName: "WalletTypes");

            migrationBuilder.RenameTable(
                name: "TransactionType",
                newName: "TransactionTypes");

            migrationBuilder.RenameTable(
                name: "TransactionStateType",
                newName: "TransactionStateTypes");

            migrationBuilder.RenameTable(
                name: "GameRoundResultType",
                newName: "GameRoundResultTypes");

            migrationBuilder.RenameTable(
                name: "GameResultType",
                newName: "GameResultTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletTypes",
                table: "WalletTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionTypes",
                table: "TransactionTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionStateTypes",
                table: "TransactionStateTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRoundResultTypes",
                table: "GameRoundResultTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameResultTypes",
                table: "GameResultTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRounds_GameRoundResultTypes_ResultId",
                table: "GameRounds",
                column: "ResultId",
                principalTable: "GameRoundResultTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameResultTypes_ResultId",
                table: "Games",
                column: "ResultId",
                principalTable: "GameResultTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionStateTypes_StateId",
                table: "Transactions",
                column: "StateId",
                principalTable: "TransactionStateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionTypes_TypeId",
                table: "Transactions",
                column: "TypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletTypes_TypeId",
                table: "Wallets",
                column: "TypeId",
                principalTable: "WalletTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRounds_GameRoundResultTypes_ResultId",
                table: "GameRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameResultTypes_ResultId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionStateTypes_StateId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionTypes_TypeId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletTypes_TypeId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WalletTypes",
                table: "WalletTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionTypes",
                table: "TransactionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionStateTypes",
                table: "TransactionStateTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRoundResultTypes",
                table: "GameRoundResultTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameResultTypes",
                table: "GameResultTypes");

            migrationBuilder.RenameTable(
                name: "WalletTypes",
                newName: "WalletType");

            migrationBuilder.RenameTable(
                name: "TransactionTypes",
                newName: "TransactionType");

            migrationBuilder.RenameTable(
                name: "TransactionStateTypes",
                newName: "TransactionStateType");

            migrationBuilder.RenameTable(
                name: "GameRoundResultTypes",
                newName: "GameRoundResultType");

            migrationBuilder.RenameTable(
                name: "GameResultTypes",
                newName: "GameResultType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WalletType",
                table: "WalletType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionType",
                table: "TransactionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionStateType",
                table: "TransactionStateType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRoundResultType",
                table: "GameRoundResultType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameResultType",
                table: "GameResultType",
                column: "Id");

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
                name: "FK_Transactions_TransactionStateType_StateId",
                table: "Transactions",
                column: "StateId",
                principalTable: "TransactionStateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionType_TypeId",
                table: "Transactions",
                column: "TypeId",
                principalTable: "TransactionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletType_TypeId",
                table: "Wallets",
                column: "TypeId",
                principalTable: "WalletType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
