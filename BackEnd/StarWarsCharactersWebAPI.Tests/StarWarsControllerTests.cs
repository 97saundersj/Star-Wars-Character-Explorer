using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using StarWarsCharactersWebAPI.Models;
using Xunit;

namespace StarWarsCharactersWebAPI.Tests;

public class StarWarsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public StarWarsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetAllCharacters_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/StarWars/characters");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAllCharacters_WithLimit_ReturnsCorrectNumberOfItems()
    {
        // Arrange
        const int limit = 5;

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters?limit={limit}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<StarWarsResponse>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(limit, result.info.limit);
        Assert.Equal(limit, result.data.Count);
    }

    [Fact]
    public async Task GetAllCharacters_WithSearch_ReturnsFilteredResults()
    {
        // Arrange
        const string searchTerm = "skywalker";

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters?search={searchTerm}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<StarWarsResponse>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.All(result.data, character => 
            Assert.True(
                character.name.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()) ||
                (character.description?.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant()) ?? false)
            )
        );
    }

    [Fact]
    public async Task GetCharacterById_WithValidId_ReturnsCharacter()
    {
        // Arrange
        // First get a valid ID from the characters list
        var listResponse = await _client.GetAsync("/api/StarWars/characters");
        var listContent = await listResponse.Content.ReadAsStringAsync();
        var listResult = JsonSerializer.Deserialize<StarWarsResponse>(listContent);
        var validId = listResult.data.First()._id;

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters/{validId}");
        var content = await response.Content.ReadAsStringAsync();
        var character = JsonSerializer.Deserialize<StarWarsCharacter>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(character);
        Assert.Equal(validId, character._id);
    }

    [Fact]
    public async Task GetCharacterById_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        const string invalidId = "invalid-id";

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAllCharacters_WithLargeLimit_ReturnsAllCharacters()
    {
        // Arrange
        const int largeLimit = 1000;

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters?limit={largeLimit}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<StarWarsResponse>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(result.info.total, result.data.Count);
        Assert.Null(result.info.next); // Should be no next page
    }

    [Fact]
    public async Task GetAllCharacters_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        const int page = 2;
        const int limit = 5;

        // Act
        var response = await _client.GetAsync($"/api/StarWars/characters?page={page}&limit={limit}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<StarWarsResponse>(content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(page, result.info.page);
        Assert.Equal(limit, result.info.limit);
        Assert.NotNull(result.info.prev); // Should have previous page
    }
} 