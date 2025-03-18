using System.Text.Json.Serialization;

namespace DataValidationApi.Models;

public record AccountValidationTimedResult : BaseValidationResult
{
        [JsonPropertyOrder(2)]
        [JsonPropertyName("invalidLines")]
        public List<string> TimedLines { get; }

        public AccountValidationTimedResult(List<string> timedLines, bool isValid) : base(isValid)
        {
            TimedLines = timedLines;
        }
    
}