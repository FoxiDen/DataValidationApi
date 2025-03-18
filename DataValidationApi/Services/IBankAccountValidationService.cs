using DataValidationApi.Models;

namespace DataValidationApi.Services;

public interface IBankAccountValidationService
{
    Task<BaseValidationResult> ValidateAccountsData(IFormFile input);
}