using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class RenameTransactionReceiverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReceivers_TransactionReceiverType_TypeId",
                table: "TransactionReceivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionReceiverType",
                table: "TransactionReceiverType");

            migrationBuilder.RenameTable(
                name: "TransactionReceiverType",
                newName: "TransactionReceiverTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionReceiverTypes",
                table: "TransactionReceiverTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReceivers_TransactionReceiverTypes_TypeId",
                table: "TransactionReceivers",
                column: "TypeId",
                principalTable: "TransactionReceiverTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionReceivers_TransactionReceiverTypes_TypeId",
                table: "TransactionReceivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionReceiverTypes",
                table: "TransactionReceiverTypes");

            migrationBuilder.RenameTable(
                name: "TransactionReceiverTypes",
                newName: "TransactionReceiverType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionReceiverType",
                table: "TransactionReceiverType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionReceivers_TransactionReceiverType_TypeId",
                table: "TransactionReceivers",
                column: "TypeId",
                principalTable: "TransactionReceiverType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
