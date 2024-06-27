using App.Services.Telegram.Interfaces;

namespace App.Services.Telegram.Payloads; 

public class TgmMessageResponse : ITgmMessageResponse {
    public int MessageId { get; set; }
    public int MessageThreadId { get; set; }
}