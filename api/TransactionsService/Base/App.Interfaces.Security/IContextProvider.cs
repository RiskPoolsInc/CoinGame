namespace App.Interfaces.Security;

public interface IContextProvider {
    ICurrentRequestClient Context { get; }
    IRequestInfo Request { get; }
    public string GetApiKey();
}