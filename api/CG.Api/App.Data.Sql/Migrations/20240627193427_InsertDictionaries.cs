using App.Data.Sql.Core;

using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class InsertDictionaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //
            // public DbSet<GameResultType> GameResultTypes { get; set; }
            // public DbSet<GameRoundResultType> GameRoundResultTypes { get; set; }
            // public DbSet<TransactionType> TransactionTypes { get; set; }
            // public DbSet<TransactionStateType> TransactionStateTypes { get; set; }
            // public DbSet<WalletType> WalletTypes { get; set; }
            // public DbSet<TransactionReceiverType> TransactionReceiverTypes { get; set; }
            // public DbSet<GameStateType> GameStateTypes { get; set; }

            #region GameResultType
            // Undefined = 1,
            //    Win = 2,
            //    Lose = 3
            migrationBuilder.InsertDictionaryItem("GameResultTypes", 1, "Undefined", "Undefined");
            migrationBuilder.InsertDictionaryItem("GameResultTypes", 2, "Win", "Win");
            migrationBuilder.InsertDictionaryItem("GameResultTypes", 3, "Lose", "Lose");
            #endregion
            #region GameRoundResultType
            //Win = 1,
            // Lose = 2
            migrationBuilder.InsertDictionaryItem("GameRoundResultTypes", 1, "Win", "Win");
            migrationBuilder.InsertDictionaryItem("GameRoundResultTypes", 2, "Lose", "Lose");
            #endregion 
            #region TransactionType
            //GameDeposit = 1,
            // UserReward = 2,
            // UserRefund = 3,
            // Service = 4,
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 1, "GameDeposit", "Game Deposit");
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 2, "UserReward", "User Reward");
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 3, "UserRefund", "User Refund");
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 4, "Service", "Service");

            #endregion
            #region TransactionStateType
            // Created = 1,
            //    Completed = 2
            migrationBuilder.InsertDictionaryItem("TransactionStateTypes", 1, "Created", "Created");
            migrationBuilder.InsertDictionaryItem("TransactionStateTypes", 2, "Completed", "Completed");
            #endregion
            #region WalletType
            // Game = 1,
            //    GameDeposit = 2,
            //    Service = 3
            migrationBuilder.InsertDictionaryItem("WalletTypes", 1, "Game", "Game");
            migrationBuilder.InsertDictionaryItem("WalletTypes", 2, "GameDeposit", "Game Deposit");
            migrationBuilder.InsertDictionaryItem("WalletTypes", 3, "Service", "Service");
            #endregion
            #region TransactionReceiverType
            //GameDeposit = 1,
            // Service = 2,
            // User = 3,
            migrationBuilder.InsertDictionaryItem("TransactionReceiverTypes", 1, "GameDeposit", "Game Deposit");
            migrationBuilder.InsertDictionaryItem("TransactionReceiverTypes", 2, "Service", "Service");
            migrationBuilder.InsertDictionaryItem("TransactionReceiverTypes", 3, "User", "User");
            #endregion
            #region GameStateType
            // Created = 1,
            // InProgress = 2,
            // Completed = 3
            migrationBuilder.InsertDictionaryItem("GameStateTypes", 1, "Created", "Created");
            migrationBuilder.InsertDictionaryItem("GameStateTypes", 2, "InProgress", "In Progress");
            migrationBuilder.InsertDictionaryItem("GameStateTypes", 3, "Completed", "Completed");
            #endregion
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
