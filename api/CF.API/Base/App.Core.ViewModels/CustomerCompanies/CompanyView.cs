using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.CustomerCompanies;

public class CompanyView : CustomerCompanyView
{
    public UserTinyView[] Users { get; set; }
}