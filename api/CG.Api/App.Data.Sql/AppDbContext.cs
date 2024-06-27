using App.Data.Entities;
using App.Data.Entities.Attachments;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Games;
using App.Data.Entities.Notifications;
using App.Data.Entities.Payments;
using App.Data.Entities.TransactionReceiver;
using App.Data.Entities.UserProfiles;
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

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionReceiver> TransactionReceivers { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<GameRound> GameRounds { get; set; }

    #endregion

    #region Dictionaries

    public DbSet<ObjectType> ObjectTypes { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<UserLogType> UserLogType { get; set; }
    public DbSet<AuditEventType> AuditEventTypes { get; set; }
    public DbSet<FollowType> FollowTypes { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<AttachmentType> AttachmentTypes { get; set; }
    public DbSet<ObjectValueType> ObjectValueTypes { get; set; }
    
    public DbSet<GameResult> GameResults { get; set; }
    public DbSet<GameState> GameStates { get; set; }
    public DbSet<TransactionReceiverType> TransactionReceiverTypes { get; set; }
    
    #endregion
}