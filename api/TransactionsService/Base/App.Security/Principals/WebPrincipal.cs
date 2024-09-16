using System.Security.Claims;

using App.Interfaces.Repositories.ServiceProfiles;
using App.Interfaces.Security;

namespace App.Security.Principals;

public class WebPrincipal : ICurrentRequestClient {
    private readonly ClaimsIdentity _identity;
    private readonly string _apiKey;
    private readonly IServiceProfileRepository _serviceProfileRepository;

    public WebPrincipal(string apiKey, IServiceProfileRepository serviceProfileRepository) {
        _apiKey = apiKey;
        _serviceProfileRepository = serviceProfileRepository;
        ProfileId = serviceProfileRepository.Where(a => a.ApiKey == _apiKey).FirstOrDefault().Id;
    }

    public bool IsAnonymous => false;
    public Guid? ProfileId { get; set; }
}