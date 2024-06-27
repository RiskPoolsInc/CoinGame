﻿// <auto-generated />
using System;
using App.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace App.Data.Sql.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("App.Data.Entities.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("character varying(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.AttachmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AttachmentTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.AuditEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AuditEventTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.FollowType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FollowTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.GameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GameResults");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.GameRoundResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GameRoundResult");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.GameState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.ObjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.ObjectValueType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ObjectValueTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.TransactionReceiverType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionReceiverTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.TransactionState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionState");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.UserLogType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserLogType");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("App.Data.Entities.Dictionaries.WalletType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WalletType");
                });

            modelBuilder.Entity("App.Data.Entities.GameRounds.GameRound", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("GameId1")
                        .HasColumnType("uuid");

                    b.Property<string>("GeneratedValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("ResultId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GameId1");

                    b.HasIndex("ResultId");

                    b.ToTable("GameRounds");
                });

            modelBuilder.Entity("App.Data.Entities.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("ResultId")
                        .HasColumnType("integer");

                    b.Property<int>("RoundQuantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("RoundSum")
                        .HasColumnType("numeric");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.HasIndex("StateId");

                    b.HasIndex("WalletId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.Follow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("TypeId");

                    b.ToTable("Follows");

                    b.HasDiscriminator<int>("TypeId");
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("JsonObjectView")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RecipientId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("RecipientId");

                    b.HasIndex("TypeId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("App.Data.Entities.TransactionReceiver.TransactionReceiver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("WalletHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("TypeId");

                    b.ToTable("TransactionReceivers");
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("ExistInBlockChain")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Fee")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uuid");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<string>("TransactionHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("WalletFromId")
                        .HasColumnType("uuid");

                    b.Property<string>("WalletHashFrom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("StateId");

                    b.HasIndex("TypeId");

                    b.HasIndex("WalletFromId");

                    b.ToTable("Transactions");

                    b.HasDiscriminator<int>("TypeId");
                });

            modelBuilder.Entity("App.Data.Entities.UserProfiles.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UbikiriUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UbikiriUserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("App.Data.Entities.Wallets.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RefundId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RefundId");

                    b.HasIndex("TypeId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.UserFollow", b =>
                {
                    b.HasBaseType("App.Data.Entities.Notifications.Follow");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.TransactionGameDeposit", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.TransactionService", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.TransactionUserRefund", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.TransactionUserReward", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.Transaction");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("App.Data.Entities.AuditLog", b =>
                {
                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Data.Entities.Dictionaries.AuditEventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("App.Data.Entities.GameRounds.GameRound", b =>
                {
                    b.HasOne("App.Data.Entities.Games.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Games.Game", null)
                        .WithMany("GameRounds")
                        .HasForeignKey("GameId1");

                    b.HasOne("App.Data.Entities.Dictionaries.GameRoundResult", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Games.Game", b =>
                {
                    b.HasOne("App.Data.Entities.Dictionaries.GameResult", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Data.Entities.Dictionaries.GameState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Wallets.Wallet", "Wallet")
                        .WithMany("Games")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.Follow", b =>
                {
                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.FollowType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.Notification", b =>
                {
                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.AuditEventType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.TransactionReceiver.TransactionReceiver", b =>
                {
                    b.HasOne("App.Data.Entities.Transactions.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionReceiverType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.Transaction", b =>
                {
                    b.HasOne("App.Data.Entities.Games.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Wallets.Wallet", "WalletFrom")
                        .WithMany()
                        .HasForeignKey("WalletFromId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Data.Entities.UserProfiles.UserProfile", b =>
                {
                    b.HasOne("App.Data.Entities.Dictionaries.UserType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Wallets.Wallet", b =>
                {
                    b.HasOne("App.Data.Entities.Transactions.TransactionUserRefund", "Refund")
                        .WithMany()
                        .HasForeignKey("RefundId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Data.Entities.Dictionaries.WalletType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Notifications.UserFollow", b =>
                {
                    b.HasOne("App.Data.Entities.UserProfiles.UserProfile", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
