using App.Data.Sql.Core;

using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class InsertDictionaries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region TransactionType
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 1, "Base", "Base");
            migrationBuilder.InsertDictionaryItem("TransactionTypes", 2, "Refund", "Refund");
            #endregion
            #region TransactionStateType
            migrationBuilder.InsertDictionaryItem("TransactionStateTypes", 1, "Created", "Created");
            migrationBuilder.InsertDictionaryItem("TransactionStateTypes", 2, "Completed", "Completed");
            #endregion
            #region WalletType
            migrationBuilder.InsertDictionaryItem("WalletTypes", 1, "Generated", "Generated");
            migrationBuilder.InsertDictionaryItem("WalletTypes", 2, "Imported", "Imported");
            #endregion
            #region TransactionReceiverType
            migrationBuilder.InsertDictionaryItem("TransactionReceiverTypes", 1, "Base", "Base");
            #endregion

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
