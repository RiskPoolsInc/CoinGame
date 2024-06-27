using System.Text.Json.Serialization;

namespace App.Services.Telegram.Interfaces; 

public interface ITgmMessageResponse {
    [JsonPropertyName("message_id")]
    public int MessageId { get; set; }

    [JsonPropertyName("message_thread_id")]
    public int MessageThreadId { get; set; }
}