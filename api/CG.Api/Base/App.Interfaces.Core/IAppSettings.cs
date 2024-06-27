namespace App.Interfaces.Core;

public interface IAppSettings : ISettingName
{
    public IRejectingSettings BlockUserRejects { get; set; }
    public IWebAppSettings Web { get; set; }
}