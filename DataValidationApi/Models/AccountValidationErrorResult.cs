using System.Text.Json.Serialization;

namespace DataValidationApi.Models;

public record AccountValidationErrorResult : BaseValidationResult
{
    [JsonPropertyOrder(2)]
    [JsonPropertyName("invalidLines")]
    public List<string> Errors { get; }

    public AccountValidationErrorResult(List<string> errors) : base(false)
    {
        Errors = errors;
    }
}