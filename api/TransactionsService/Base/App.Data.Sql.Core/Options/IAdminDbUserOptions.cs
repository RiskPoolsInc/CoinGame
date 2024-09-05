using App.Interfaces.Core.Configurations;

namespace App.Data.Sql.Core.Options; 

public interface IAdminDbUserOptions : IConfig {
    public string AdminId { get; set; }
    public string CompanyId { get; set; }
}