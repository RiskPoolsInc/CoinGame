using System.Text.Json.Serialization;

namespace App.Services.Telegram.Interfaces; 

public interface ITgmMessageEntity {
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("custom_emoji_id")]
    public string CustomEmojiId { get; set; }
}