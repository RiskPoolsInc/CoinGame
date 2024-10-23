using System.Text.Json.Serialization;

using Telegram.Bot.Types;

namespace App.Services.Telegram.Payloads; 

public class ChatTgmMessage : TgmMessage {
    public ChatTgmMessage(long chatId) {
        ChatId = chatId.ToString();
    }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("chat_id")]
    public string ChatId { get; set; }

    /// <summary>
    ///     Mode of formatting style message
    /// </summary>
    /// <value>possible values: null, MarkdownV2, HTML, Markdown</value>
    /// <see href="https://core.telegram.org/bots/api#formatting-options" />
    [JsonPropertyName("parse_mode")]
    public string? ParseMode { get; set; }

    [JsonPropertyName("entities")]
    public MessageEntity[]? Entities { get; set; }

    [JsonPropertyName("disable_web_page_preview")]
    public bool? DisableWebPagePreview { get; set; }
}