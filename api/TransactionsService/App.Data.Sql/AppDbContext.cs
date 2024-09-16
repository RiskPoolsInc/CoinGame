using App.Data.Entities.Dictionaries;
using App.Data.Entities.ServiceProfiles;
using App.Data.Entities.TransactionReceivers;
using App.Data.Entities.Transactions;
using App.Data.Entities.Wallets;
using App.Data.Sql.Core;
using App.Data.Sql.Core.Configuration;
using App.Interfaces.Data.Sql;

using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql;

public class AppDbContext : BaseDbContext, IAppDbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        DbConfigurator.Configure<AppDbContext>(modelBuilder);
    }

    #region Entities

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<BaseTransaction> Transactions { get; set; }
    public DbSet<ServiceProfile> ServiceProfiles { get; set; }
    public DbSet<TransactionReceiver> TransactionReceivers { get; set; }

    #endregion

    #region Dictionaries

    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<TransactionStateType> TransactionStateTypes { get; set; }
    public DbSet<WalletType> WalletTypes { get; set; }
    public DbSet<TransactionReceiverType> TransactionReceiverTypes { get; set; }

    #endregion
}