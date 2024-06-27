using System.Text.Json.Serialization;

namespace App.Core.Exceptions;

public class ExternalRequestException : Exception {
    [JsonPropertyName("errors")]
    public ExternalRequestPartException[] Errors { get; set; }

    [JsonPropertyName("code")]
    public object Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}