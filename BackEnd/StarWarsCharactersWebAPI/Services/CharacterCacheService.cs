using Microsoft.Extensions.Caching.Memory;
using StarWarsCharactersWebAPI.Models;
using StarWarsCharactersWebAPI.Services.Interfaces;
using System.Text.Json;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterCacheService(IMemoryCache cache, HttpClient httpClient) : ICharacterCacheService
    {
        private readonly IMemoryCache _cache = cache;
        private readonly HttpClient _httpClient = httpClient;
        private const string BaseUrl = "https://starwars-databank-server.vercel.app/api/v1";
        private const int InitialFetchLimit = 1000;
        private const int DefaultCacheExpirationMinutes = 15;
        private const int CharacterCacheExpirationHours = 1;

        public async Task<StarWarsResponse> GetAllCharactersAsync()
        {
            const string cacheKey = "all_characters";

            // Try to get from cache first
            if (_cache.TryGetValue(cacheKey, out StarWarsResponse cachedData))
            {
                return cachedData;
            }

            // If not in cache, fetch the data
            var data = await FetchAllCharactersAsync();

            if (data != null)
            {
                // Store in cache with expiration
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(DefaultCacheExpirationMinutes));
                _cache.Set(cacheKey, data, cacheOptions);
            }

            return data;
        }

        public async Task<StarWarsCharacter> GetCharacterByIdAsync(string id)
        {
            string cacheKey = $"character_{id}";

            // Try to get from cache first
            if (_cache.TryGetValue(cacheKey, out StarWarsCharacter cachedData))
            {
                return cachedData;
            }

            // If not in cache, fetch the data
            var data = await FetchCharacterByIdAsync(id);

            if (data != null)
            {
                // Store in cache with expiration
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(CharacterCacheExpirationHours));
                _cache.Set(cacheKey, data, cacheOptions);
            }

            return data;
        }

        private async Task<StarWarsResponse> FetchAllCharactersAsync()
        {
            var allData = new List<StarWarsCharacter>();
            int currentPage = 1;
            StarWarsResponse? pageResult;

            do
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/characters?page={currentPage}&limit={InitialFetchLimit}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                pageResult = JsonSerializer.Deserialize<StarWarsResponse>(content);

                if (pageResult?.Data != null)
                    allData.AddRange(pageResult.Data);

                currentPage++;
            } while (pageResult?.Info != null && currentPage <= (int)Math.Ceiling((double)pageResult.Info.Total / pageResult.Info.Limit));

            return new StarWarsResponse
            {
                Info = new StarWarsInfo
                {
                    Total = allData.Count,
                    Page = 1,
                    Limit = allData.Count,
                    Next = null,
                    Prev = null
                },
                Data = allData
            };
        }

        private async Task<StarWarsCharacter> FetchCharacterByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/characters/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    return null;

                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<StarWarsCharacter>(content);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}