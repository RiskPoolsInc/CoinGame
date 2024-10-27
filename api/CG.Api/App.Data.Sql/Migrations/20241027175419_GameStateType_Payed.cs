using App.Data.Sql.Core;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class GameStateType_Payed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertDictionaryItem("GameStateTypes", 4, "Payed", "Payed");
            migrationBuilder.Sql(@"UPDATE ""Games"" SET ""StateId"" = 4 WHERE ""CreatedOn"" < ""10/27/2024""");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
