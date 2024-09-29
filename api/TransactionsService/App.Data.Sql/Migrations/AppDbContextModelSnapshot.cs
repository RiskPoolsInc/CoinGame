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

            modelBuilder.Entity("App.Data.Entities.Dictionaries.TransactionStateType", b =>
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

                    b.ToTable("TransactionStateTypes");
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

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("App.Data.Entities.ServiceProfiles.ServiceProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("ServiceProfiles");
                });

            modelBuilder.Entity("App.Data.Entities.TransactionReceivers.TransactionReceiver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FromAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.HasIndex("TypeId");

                    b.ToTable("TransactionReceivers");
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.BaseTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Error")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ExistInBlockChain")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Fee")
                        .HasColumnType("numeric");

                    b.Property<string>("Hash")
                        .HasColumnType("text");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("WalletId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("StateId");

                    b.HasIndex("TypeId");

                    b.HasIndex("WalletId");

                    b.ToTable("Transactions");

                    b.HasDiscriminator<int>("TypeId");
                });

            modelBuilder.Entity("App.Data.Entities.Wallets.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEncrypted")
                        .HasColumnType("boolean");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("TypeId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.Transaction", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.BaseTransaction");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.TransactionRefund", b =>
                {
                    b.HasBaseType("App.Data.Entities.Transactions.BaseTransaction");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("App.Data.Entities.TransactionReceivers.TransactionReceiver", b =>
                {
                    b.HasOne("App.Data.Entities.Transactions.BaseTransaction", "BaseTransaction")
                        .WithMany("Receivers")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionReceiverType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("App.Data.Entities.Transactions.BaseTransaction", b =>
                {
                    b.HasOne("App.Data.Entities.ServiceProfiles.ServiceProfile", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionStateType", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.TransactionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Wallets.Wallet", "Wallet")
                        .WithMany("Transactions")
                        .HasForeignKey("WalletId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("App.Data.Entities.Wallets.Wallet", b =>
                {
                    b.HasOne("App.Data.Entities.ServiceProfiles.ServiceProfile", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Data.Entities.Dictionaries.WalletType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}