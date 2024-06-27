using App.Data.Sql.Core.Interfaces;

using Microsoft.Extensions.Configuration;

namespace App.Data.Sql.Core.Providers;

public class MigrationConfigProvider : IMigrationConfig {
    public static string SETTING_NAME = "MigrationConfig";
    public bool MigrateAfterBuild { get; set; }

    public static MigrationConfigProvider Get(IConfiguration configuration) {
        bool.TryParse(configuration[$"{SETTING_NAME}:{nameof(MigrateAfterBuild)}"], out var migrate);
        return new MigrationConfigProvider { MigrateAfterBuild = migrate };
    }
}