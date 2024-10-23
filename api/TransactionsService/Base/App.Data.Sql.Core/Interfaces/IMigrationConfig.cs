namespace App.Data.Sql.Core.Interfaces; 

public interface IMigrationConfig : ISettingName {
    public bool MigrateAfterBuild { get; set; }
}