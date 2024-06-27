using App.Core.Configurations.Annotaions;

namespace App.Data.Sql.Core.Options; 

[ConfigurationName(nameof(AdminDbUserOptions))]
public class AdminDbUserOptions : IAdminDbUserOptions {
    public string AdminId { get; set; }
    public string CompanyId { get; set; }
}