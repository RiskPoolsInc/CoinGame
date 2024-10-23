using System.Text.Json.Serialization;

namespace App.Core.Exceptions;

public class ExternalRequestPartException {
    [JsonPropertyName("message")]
    public string Message { get; set; }
}