using App.Interfaces.Core;

namespace App.Common;

public class WebAppSettings: IWebAppSettings
{
    public string Host { get; set; }
    public string PathToTasks { get; set; }
}