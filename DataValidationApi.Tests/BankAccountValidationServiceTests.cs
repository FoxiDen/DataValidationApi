using DataValidationApi.Models;
using DataValidationApi.Services;
using FluentAssertions;
using FluentAssertions.Execution;

namespace DataValidationApi.Tests;

public class BankAccountValidationServiceTests : BaseTests
{
    private readonly BankAccountValidationService _validationService = new();

    [Fact]
    public async Task ValidateAccountsData_ShouldReturnValidResult_WhenFileIsValid()
    {
        var fileContent = "John 3293982\nJane 3293982p";
        var fileMock = CreateMockFile(fileContent);

        var result = await _validationService.ValidateAccountsData(fileMock.Object);

        using (new AssertionScope())
        {
            result.Should().BeOfType<AccountValidationSuccessResult>();
            result.IsValid.Should().BeTrue();
        }
    }

    [Theory]
    [InlineData("JohnDoe 1234567", false, "Account name, account number - not valid for 1 line 'JohnDoe 1234567'")]
    [InlineData("JohnDoe 3293982", false, "Account name - not valid for 1 line 'JohnDoe 3293982'")]
    [InlineData("John 52939822", false, "Account number - not valid for 1 line 'John 52939822'")]
    public async Task ValidateAccountsData_ShouldReturnErrorResult_WhenFileContainsInvalidData(string fileContent, bool isValid, string expectedError)
    {
        var fileMock = CreateMockFile(fileContent);

        var result = await _validationService.ValidateAccountsData(fileMock.Object);

        using (new AssertionScope())
        {
            result.Should().BeOfType<AccountValidationErrorResult>();
            result.IsValid.Should().Be(isValid);

            var errorResult = result as AccountValidationErrorResult;
            errorResult!.Errors.Should().Contain(expectedError);
        }
    }

    //Test for given example in the task.
    [Theory]
    [InlineData("Thomas 32999921\nRichard 3293982\nXAEA-12 8293982\nRose 329a982\nBob 329398.\nmichael 3113902\nRob 3113902p", 
        "Account number - not valid for 1 line 'Thomas 32999921'",
        "Account name, account number - not valid for 3 line 'XAEA-12 8293982'",
        "Account number - not valid for 4 line 'Rose 329a982'",
        "Account number - not valid for 5 line 'Bob 329398.'",
        "Account name - not valid for 6 line 'michael 3113902'")]
    public async Task ValidateAccountsData_ShouldReturnError_WhenFileContainsMultipleInvalidData(string fileContent, params string[] expectedErrors)
    {
        var fileMock = CreateMockFile(fileContent);

        var result = await _validationService.ValidateAccountsData(fileMock.Object);

        using (new AssertionScope())
        {
            result.Should().BeOfType<AccountValidationErrorResult>();
            result.IsValid.Should().BeFalse();

            var errorResult = result as AccountValidationErrorResult;
            errorResult!.Errors.Count.Should().Be(5);
            foreach (var error in expectedErrors)
            {
                errorResult.Errors.Should().Contain(error);
            }
        }
    }
    
    [Fact]
    public async Task ValidateAccountsData_ShouldReturnError_WhenFileIsEmpty()
    {
        var fileMock = CreateMockFile(string.Empty); 

        var result = await _validationService.ValidateAccountsData(fileMock.Object);

        using (new AssertionScope())
        {
            result.Should().BeOfType<AccountValidationErrorResult>();
            result.IsValid.Should().BeFalse();

            var errorResult = result as AccountValidationErrorResult;
            errorResult!.Errors.Should().Contain("No file provided or empty file.");
        }
    }

    [Fact]
    public async Task ValidateAccountsData_ShouldReturnError_WhenFileHasInvalidFormat()
    {
        var fileContent = "InvalidAccountFormat";
        var fileMock = CreateMockFile(fileContent);

        var result = await _validationService.ValidateAccountsData(fileMock.Object);

        using (new AssertionScope())
        {
            result.Should().BeOfType<AccountValidationErrorResult>();
            result.IsValid.Should().BeFalse();
            
            var errorResult = result as AccountValidationErrorResult;
            errorResult!.Errors.Should().Contain("Line 1 - Invalid format, should contain an account name and number for line 'InvalidAccountFormat'");
        }
    }
}
