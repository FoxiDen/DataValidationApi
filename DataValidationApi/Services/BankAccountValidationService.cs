using System.Text.RegularExpressions;
using DataValidationApi.Helpers;
using DataValidationApi.Models;

namespace DataValidationApi.Services;

public class BankAccountValidationService : IBankAccountValidationService
{
    public async Task<BaseValidationResult> ValidateAccountsData(IFormFile input)
    {
        if (input.Length == 0)
        {
            return new AccountValidationErrorResult(["No file provided or empty file."]);
        }

        using var reader = new StreamReader(input.OpenReadStream());
        var inputData = await reader.ReadToEndAsync();
        
        return ValidateAccountDataInternal(inputData);
    }
    
    private static BaseValidationResult ValidateAccountDataInternal(string input)
    {
        var lines = input.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
        var errors = new List<string>();
        var lineNumber = 1;

        foreach (var line in lines)
        {
            var validationMessage = ValidateLine(line, lineNumber);
            if (validationMessage != null)
            {
                errors.Add(validationMessage);
            }
            lineNumber++;
        }

        return errors.Any()
            ? new AccountValidationErrorResult(errors)
            : new AccountValidationSuccessResult();
    }

    private static string? ValidateLine(string line, int lineNumber)
    {
        var lineParts = line.Split(' ');

        if (lineParts.Length != 2)
        {
            return ValidationMessageHelper.FormatInvalidFormatMessage(line, lineNumber);
        }

        var accountName = lineParts[0];
        var accountNumber = lineParts[1];
        
        var validationIssues = new List<string>();

        if (!IsValidAccountName(accountName))
        {
            validationIssues.Add("Account name");
        }

        if (!IsValidAccountNumber(accountNumber))
        {
            validationIssues.Add("Account number");
        }

        return validationIssues.Count == 0 ? 
            null :
            ValidationMessageHelper.FormatValidationFailureMessage(line, lineNumber, validationIssues);
    }
    
    private static bool IsValidAccountName(string name)
    {
        var nameRegex = new Regex("^[A-Z][a-z]+$");
        return nameRegex.IsMatch(name);
    }

    private static bool IsValidAccountNumber(string accountNumber)
    {
        var accountNumberRegex = new Regex(@"^[3-4]\d{6}(p)?$");
        return accountNumberRegex.IsMatch(accountNumber);
    }
}