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

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            const string cacheKey = "all_characters";
            return await GetOrSetCacheAsync(cacheKey, FetchAllCharactersAsync, DefaultCacheExpirationMinutes) ?? [];
        }

        private async Task<T?> GetOrSetCacheAsync<T>(string cacheKey, Func<Task<T?>> fetchData, int expirationMinutes)
        {
            if (_cache.TryGetValue(cacheKey, out T? cachedData))
            {
                return cachedData;
            }

            var data = await fetchData();
            if (data != null)
            {
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes));
                _cache.Set(cacheKey, data, cacheOptions);
            }

            return data;
        }

        private async Task<List<Character>?> FetchAllCharactersAsync()
        {
            var allData = new List<Character>();
            int currentPage = 1;
            CharacterResponse? pageResult;
            int totalPages = 0;

            do
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/characters?page={currentPage}&limit={InitialFetchLimit}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                pageResult = JsonSerializer.Deserialize<CharacterResponse>(content);

                if (pageResult?.Data != null)
                {
                    allData.AddRange(pageResult.Data);
                }

                if (pageResult?.Info != null)
                {
                    totalPages = (int)Math.Ceiling((double)pageResult.Info.Total / pageResult.Info.Limit);
                }

                currentPage++;
            } while (pageResult?.Info != null && currentPage <= totalPages);

            return allData;
        }
    }
}