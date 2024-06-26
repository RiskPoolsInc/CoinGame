using Newtonsoft.Json;

namespace App.Core.Commands.Telegram;

public class TgmMessageEntityCommand {
    /// <summary>
    ///     Type of the entity
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Type { get; set; }

    /// <summary>
    ///     Offset in UTF-16 code units to the start of the entity
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Offset { get; set; }

    /// <summary>
    ///     Length of the entity in UTF-16 code units
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Length { get; set; }

    /// <summary>
    ///     Optional. For <see cref="MessageEntityType.TextLink" /> only, url that will be opened after user taps on the text
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Url { get; set; }

    /// <summary>
    ///     Optional. For <see cref="MessageEntityType.TextMention" /> only, the mentioned user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public TgmUser? User { get; set; }

    /// <summary>
    ///     Optional. For <see cref="MessageEntityType.Pre" /> only, the programming language of the entity text
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Language { get; set; }
}