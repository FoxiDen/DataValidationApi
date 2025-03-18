using System.Text.Json.Serialization;

namespace DataValidationApi.Models;

public record BaseValidationResult
{
    [JsonPropertyOrder(1)]
    [JsonPropertyName("fileValid")]
    public bool IsValid { get; }

    protected BaseValidationResult(bool isValid)
    {
        IsValid = isValid;
    }
}