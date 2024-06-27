using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Core.Configuration;

public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : class {
    public void Configure(ModelBuilder modelBuilder) {
        Configure(modelBuilder.Entity<TEntity>());
    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);

    protected void ConfigurePropertiesColumnType<T>(EntityTypeBuilder<TEntity> builder, string sqlColumnType) {
        var properties = typeof(TEntity).GetProperties().Where(a => a.CanRead && a.CanWrite && a.PropertyType == typeof(T)).ToArray();
        Array.ForEach(properties, property => builder.Property(property.Name).HasColumnType(sqlColumnType));
    }
}