namespace App.Services.Telegram.User; 

/// <summary>
///     Data from message sent to after buy Premium subscription
/// </summary>
public class TelegramPremiumAccountData {
    public TelegramPremiumAccountData(string phone, string instanceId, string clientId, string clientSecret) {
        Phone = phone;
        InstanceId = instanceId;
        ClientId = clientId;
        ClientSecret = clientSecret;
    }

    public string Phone { get; }
    public string InstanceId { get; }
    public string ClientId { get; }
    public string ClientSecret { get; }
}