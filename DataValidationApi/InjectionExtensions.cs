using DataValidationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataValidationApi;

public static class InjectionExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IBankAccountValidationService, BankAccountValidationService>();
    }
    
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapValidationEndpoints();
    }
    
    private static void MapValidationEndpoints(this WebApplication app)
    {
        var validationGroup = app.MapGroup("/validate").DisableAntiforgery();
        
        validationGroup.MapPost("/accounts", async (IBankAccountValidationService validationService, IFormFile file, [FromQuery] bool timed = false) =>
        {
            var validationResult = await validationService.ValidateAccountsData(file, timed);

            return validationResult.IsValid
                ? Results.Ok(validationResult)
                : Results.BadRequest(validationResult);
        });
    }
}