using EffectiveMobile.AdPlatforms.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace EffectiveMobile.AdPlatforms.Tests.IntegrationTests;

public class PlatformsUploadTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private const string RequestUri = "api/platforms/upload";
    private const string UploadFileParameter = "locations";

    public PlatformsUploadTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Upload_DefaultFileSuccessTest()
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
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task Upload_EmptyFileSuccessTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = string.Empty;
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Upload_MissingSplitSymbolFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = "text without split symbol"; // Without ':'
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task Upload_MissingLocationsFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var content = "MissingLocations:";
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task Upload_MissingPlatformFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string content = ":MissingPlatform";
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task Upload_MissingSlashSymbolFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string content = "testLocation:testPlatform";
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
    
    [Fact]
    public async Task Upload_ExtraSplitSymbolFailureTest()
    {
        // Arrange
        var client = _factory.CreateClient();
        const string content = "testLocation:test:Platform";
        const string filename = "test.txt";
        var fileBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(fileBytes);
        
        var form = new MultipartFormDataContent();
        form.Add(byteContent, UploadFileParameter, filename);
        
        // Act
        var response = await client.PostAsync(RequestUri, form);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }
}