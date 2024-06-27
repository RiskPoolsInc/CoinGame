namespace App.Interfaces.Core;

public interface IWebAppSettings
{
    string Host { get; }
    string PathToTasks { get; }
}