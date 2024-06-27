using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Sql.Migrations
{
    public partial class RenameGameStateType_FollowType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_FollowType_TypeId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStateType_StateId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameStateType",
                table: "GameStateType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowType",
                table: "FollowType");

            migrationBuilder.RenameTable(
                name: "GameStateType",
                newName: "GameStateTypes");

            migrationBuilder.RenameTable(
                name: "FollowType",
                newName: "FollowTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameStateTypes",
                table: "GameStateTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowTypes",
                table: "FollowTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_FollowTypes_TypeId",
                table: "Follows",
                column: "TypeId",
                principalTable: "FollowTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStateTypes_StateId",
                table: "Games",
                column: "StateId",
                principalTable: "GameStateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_FollowTypes_TypeId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameStateTypes_StateId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameStateTypes",
                table: "GameStateTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowTypes",
                table: "FollowTypes");

            migrationBuilder.RenameTable(
                name: "GameStateTypes",
                newName: "GameStateType");

            migrationBuilder.RenameTable(
                name: "FollowTypes",
                newName: "FollowType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameStateType",
                table: "GameStateType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowType",
                table: "FollowType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_FollowType_TypeId",
                table: "Follows",
                column: "TypeId",
                principalTable: "FollowType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameStateType_StateId",
                table: "Games",
                column: "StateId",
                principalTable: "GameStateType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
