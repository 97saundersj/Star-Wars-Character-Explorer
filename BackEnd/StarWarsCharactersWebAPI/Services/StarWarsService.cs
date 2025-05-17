using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services
{
    public interface IStarWarsService
    {
        Task<List<StarWarsCharacter>> GetAllCharactersAsync();
        Task<StarWarsCharacter> GetCharacterByIdAsync(string id);
    }

    public class StarWarsService : IStarWarsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string BaseUrl = "https://starwars-databank-server.vercel.app/api/v1";
        private const string CacheKey = "starwars_characters";

        public StarWarsService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<List<StarWarsCharacter>> GetAllCharactersAsync()
        {
            if (_cache.TryGetValue(CacheKey, out List<StarWarsCharacter> cachedCharacters))
            {
                return cachedCharacters;
            }

            var response = await _httpClient.GetAsync($"{BaseUrl}/characters");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<StarWarsResponse>(content);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _cache.Set(CacheKey, result.data, cacheOptions);

            return result.data;
        }

        public async Task<StarWarsCharacter> GetCharacterByIdAsync(string id)
        {
            var characters = await GetAllCharactersAsync();
            return characters.FirstOrDefault(c => c._id == id);
        }
    }
}