using System.Text;
using Microsoft.AspNetCore.Http;
using Moq;

namespace DataValidationApi.Tests;

public class BaseTests
{
    protected BaseTests()
    {
    }
    
    protected static Mock<IFormFile> CreateMockFile(string content)
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.Length).Returns(content.Length);
        fileMock.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes(content)));
        return fileMock;
    }
}