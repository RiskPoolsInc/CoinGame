using System.Collections.Concurrent;
using System.Reflection;

using App.Data.Sql.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core.Configuration;

public class DbConfigurator {
    private static readonly ConcurrentDictionary<Assembly, Type[]> _configurationTypes;
    private static readonly ConcurrentBag<Type> _globalConfigurationTypes;

    static DbConfigurator() {
        _configurationTypes = new ConcurrentDictionary<Assembly, Type[]>();
        _globalConfigurationTypes = new ConcurrentBag<Type>();
    }

    public static void Register(Assembly assembly) {
        var types = assembly.GetTypes()
                            .Where(a => !a.IsAbstract && !a.IsGenericType && typeof(IEntityTypeConfiguration).IsAssignableFrom(a))
                            .ToArray();
        _configurationTypes.AddOrUpdate(assembly, types, (a, o) => o);

        var globalTypes = assembly.GetTypes()
                                  .Where(a => !a.IsAbstract &&
                                             !a.IsGenericType &&
                                             typeof(IModelConfiguration).IsAssignableFrom(a) &&
                                             !_globalConfigurationTypes.Contains(a))
                                  .ToArray();

        Array.ForEach(globalTypes, t => _globalConfigurationTypes.Add(t));
    }

    public static void Configure<TContext>(ModelBuilder builder) where TContext : DbContext {
        if (_configurationTypes.TryGetValue(typeof(TContext).Assembly, out var types))
            Array.ForEach(types, t => {
                var entityTypeConfiguration = (IEntityTypeConfiguration)Activator.CreateInstance(t);
                entityTypeConfiguration?.Configure(builder);
            });

        Array.ForEach(_globalConfigurationTypes.ToArray(), t => {
            var modelConfiguration = (IModelConfiguration)Activator.CreateInstance(t);
            modelConfiguration?.Configure(builder);
        });
    }
}