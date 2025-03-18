namespace DataValidationApi.Helpers;

public static class ValidationMessageHelper
{
    public static string FormatInvalidFormatMessage(string line, int lineNumber)
    {
        return $"Line {lineNumber} - Invalid format, should contain an account name and number for line '{line}'";
    }

    public static string FormatValidationFailureMessage(string line, int lineNumber, List<string> validationIssues)
    {
        var recapitalizedValidationIssues = validationIssues.Select((x,index) => index == 0 ? x : x.ToLower()).ToList();
        var validationMessage = string.Join(", ", recapitalizedValidationIssues);
        
        return $"{validationMessage} - not valid for {lineNumber} line '{line}'";
    }
    
    public static string FormatValidationTimedMessage(int lineNumber, long validationTicks, bool isValid)
    {
        return $"Line {lineNumber} took {validationTicks} ticks to validate (lineValid: {isValid.ToString()})";
    }
}