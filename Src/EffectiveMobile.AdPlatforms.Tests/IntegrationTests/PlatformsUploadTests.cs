using EffectiveMobile.AdPlatforms.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace EffectiveMobile.AdPlatforms.Tests.IntegrationTests;

public class PlatformsUploadTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private const string RequestUri = "api/platforms/upload";

    public PlatformsUploadTests(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task Upload_SuccessTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = """
                      Яндекс.Директ:/ru
                      Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
                      Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
                      Крутая реклама:/ru/svrd
                      """;
        var filename = "test.txt";
        var filebytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(filebytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, "locations", filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
        Assert.True(response.IsSuccessStatusCode);
    }
}