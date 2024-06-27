namespace App.Interfaces.Security;

public interface IContextProvider {
    ICurrentUser Context { get; }
    IRequestInfo Request { get; }
    object GetUser { get; } //TODO Add interface
    public string GetAccessToken();
}