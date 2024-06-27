using App.Data.Sql.Core.Options;

namespace App.Data.Sql.Core.Interfaces; 

public interface IAdminDbUserOptionsProvider {
    IAdminDbUserOptions GetAdminUserData();
}