using System.Net.Http.Json;
using EffectiveMobile.AdPlatforms.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace EffectiveMobile.AdPlatforms.Tests.IntegrationTests;

public class PlatformsSearchTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private const string UploadRequestUri = "api/platforms/upload";
    private const string SearchRequestUri = "api/platforms/search";
    private const string UploadFileParameter = "locations";

    public PlatformsSearchTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Search_SuccessTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string content = """
                               Яндекс.Директ:/ru
                               Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
                               Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
                               Крутая реклама:/ru/svrd
                               """;
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        await client.PostAsync(UploadRequestUri, form);
        
        // Act
        var response = await client.GetAsync($"{SearchRequestUri}?location=/ru/msk");
        var platforms = await response.Content.ReadFromJsonAsync<string[]>();
        
        // Assert
        Assert.NotNull(platforms);
        Assert.Contains("Яндекс.Директ", platforms);
        Assert.Contains("Газета уральских москвичей", platforms);
    }

    [Fact]
    public async Task Search_EmptySuccessTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"{SearchRequestUri}?location=/ru/msk");
        var platforms = await response.Content.ReadFromJsonAsync<string[]>();
        
        // Assert
        Assert.NotNull(platforms);
        Assert.Empty(platforms);
    }
    
    [Fact]
    public async Task Search_NotFoundWithoutSlashSuccessTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = "Яндекс.Директ:/ru";
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        await client.PostAsync(UploadRequestUri, form);
        
        // Act
        var response = await client.GetAsync($"{SearchRequestUri}?location=ru");
        var platforms = await response.Content.ReadFromJsonAsync<string[]>();
        
        // Assert
        Assert.NotNull(platforms);
        Assert.DoesNotContain("Яндекс.Директ", platforms);
        Assert.Empty(platforms);
    }
    
    [Fact]
    public async Task Search_MissingLocationParameterFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync($"{SearchRequestUri}");
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
}