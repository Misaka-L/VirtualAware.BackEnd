using System.Text.Json.Serialization;

namespace VirtualAware.BackEnd.Api.Models;

public class ExceptionResponse {
    [JsonPropertyName("message")] public string Message { get; set; }
    [JsonPropertyName("exception")] public string Exception { get; set; }
}
