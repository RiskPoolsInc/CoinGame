using App.Interfaces.Security;

namespace App.Security.Metadata;

public class WebRequestInfo : IRequestInfo {
    public string IP { get; set; }
    public string UserAgent { get; set; }
}