namespace DataValidationApi.Models;

public record AccountValidationSuccessResult : BaseValidationResult
{
    public AccountValidationSuccessResult() : base(true)
    {
    }
}