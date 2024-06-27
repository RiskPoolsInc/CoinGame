namespace App.Interfaces.Security;

public interface IRequestInfo {
    string IP { get; }
    string UserAgent { get; }
}