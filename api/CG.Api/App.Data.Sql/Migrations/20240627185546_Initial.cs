using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Data.Sql.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FollowType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameRoundResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRoundResult", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionReceiverType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionReceiverType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WalletType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UbikiriUserId = table.Column<Guid>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IP = table.Column<string>(unicode: false, maxLength: 40, nullable: false),
                    CreatedById = table.Column<Guid>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_UserProfiles_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_AuditEventTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AuditEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    FollowerId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Follows_UserProfiles_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_FollowType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FollowType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Follows_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    ObjectId = table.Column<Guid>(nullable: false),
                    JsonObjectView = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    RecipientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_UserProfiles_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_UserProfiles_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_AuditEventTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AuditEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    WalletId = table.Column<Guid>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    ResultId = table.Column<int>(nullable: true),
                    RoundQuantity = table.Column<int>(nullable: false),
                    RoundSum = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameResult_ResultId",
                        column: x => x.ResultId,
                        principalTable: "GameResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_GameState_StateId",
                        column: x => x.StateId,
                        principalTable: "GameState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameRounds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    GeneratedValue = table.Column<string>(nullable: false),
                    ResultId = table.Column<int>(nullable: false),
                    GameId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameRounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameRounds_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRounds_Games_GameId1",
                        column: x => x.GameId1,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GameRounds_GameRoundResult_ResultId",
                        column: x => x.ResultId,
                        principalTable: "GameRoundResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<Guid>(nullable: true),
                    WalletFromId = table.Column<Guid>(nullable: true),
                    WalletHashFrom = table.Column<string>(nullable: false),
                    TransactionHash = table.Column<string>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    StateId = table.Column<int>(nullable: false),
                    ExistInBlockChain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionState_StateId",
                        column: x => x.StateId,
                        principalTable: "TransactionState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionReceivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    WalletHash = table.Column<string>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionReceivers_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionReceivers_TransactionReceiverType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionReceiverType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    PrivateKey = table.Column<string>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    RefundId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Transactions_RefundId",
                        column: x => x.RefundId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wallets_WalletType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "WalletType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedById",
                table: "AuditLogs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TypeId",
                table: "AuditLogs",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_TypeId",
                table: "Follows",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId",
                table: "Follows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameId",
                table: "GameRounds",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_GameId1",
                table: "GameRounds",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_GameRounds_ResultId",
                table: "GameRounds",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ResultId",
                table: "Games",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_StateId",
                table: "Games",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WalletId",
                table: "Games",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedById",
                table: "Notifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId",
                table: "Notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TypeId",
                table: "Notifications",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReceivers_TransactionId",
                table: "TransactionReceivers",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionReceivers_TypeId",
                table: "TransactionReceivers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GameId",
                table: "Transactions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StateId",
                table: "Transactions",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletFromId",
                table: "Transactions",
                column: "WalletFromId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_TypeId",
                table: "UserProfiles",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UbikiriUserId",
                table: "UserProfiles",
                column: "UbikiriUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_RefundId",
                table: "Wallets",
                column: "RefundId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_TypeId",
                table: "Wallets",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Wallets_WalletId",
                table: "Games",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletFromId",
                table: "Transactions",
                column: "WalletFromId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Games_GameId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletFromId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "GameRounds");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ObjectTypes");

            migrationBuilder.DropTable(
                name: "TransactionReceivers");

            migrationBuilder.DropTable(
                name: "UserLogType");

            migrationBuilder.DropTable(
                name: "FollowType");

            migrationBuilder.DropTable(
                name: "GameRoundResult");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AuditEventTypes");

            migrationBuilder.DropTable(
                name: "TransactionReceiverType");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "GameResult");

            migrationBuilder.DropTable(
                name: "GameState");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "WalletType");

            migrationBuilder.DropTable(
                name: "TransactionState");

            migrationBuilder.DropTable(
                name: "TransactionType");
        }
    }
}
