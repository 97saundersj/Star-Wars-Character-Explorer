using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services
{
    public interface IStarWarsService
    {
        Task<StarWarsResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string search = null);
        Task<StarWarsCharacter> GetCharacterByIdAsync(string id);
    }

    public class StarWarsService : IStarWarsService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string BaseUrl = "https://starwars-databank-server.vercel.app/api/v1";
        private const string AllCharactersCacheKey = "all_characters";

        public StarWarsService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<StarWarsResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string search = null)
        {
            if (!_cache.TryGetValue(AllCharactersCacheKey, out StarWarsResponse allCharacters))
            {
                // Fetch all pages from the external API
                var allData = new List<StarWarsCharacter>();
                int currentPage = 1;
                int totalPages = 1;
                StarWarsInfo info = null;
                do
                {
                    var response = await _httpClient.GetAsync($"{BaseUrl}/characters?page={currentPage}");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var pageResult = JsonSerializer.Deserialize<StarWarsResponse>(content);
                    if (pageResult?.data != null)
                        allData.AddRange(pageResult.data);
                    info = pageResult?.info;
                    totalPages = info != null ? (int)Math.Ceiling((double)info.total / info.limit) : 1;
                    currentPage++;
                } while (currentPage <= totalPages);

                allCharacters = new StarWarsResponse
                {
                    info = new StarWarsInfo
                    {
                        total = allData.Count,
                        page = 1,
                        limit = allData.Count,
                        next = null,
                        prev = null
                    },
                    data = allData
                };

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                _cache.Set(AllCharactersCacheKey, allCharacters, cacheOptions);
            }

            // Apply search filter if provided
            var filteredCharacters = allCharacters.data;
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLowerInvariant();
                filteredCharacters = filteredCharacters
                    .Where(c => c.name.ToLowerInvariant().Contains(search) || 
                              (c.description?.ToLowerInvariant().Contains(search) ?? false))
                    .ToList();
            }

            // Calculate pagination
            var totalItems = filteredCharacters.Count;
            var pageSize = limit ?? 10;
            var totalPagesLocal = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPagesLocal));

            var pagedCharacters = filteredCharacters
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Create response with pagination info
            return new StarWarsResponse
            {
                info = new StarWarsInfo
                {
                    total = totalItems,
                    page = page,
                    limit = pageSize,
                    next = page < totalPagesLocal ? $"/api/StarWars/characters?page={page + 1}&limit={pageSize}" : null,
                    prev = page > 1 ? $"/api/StarWars/characters?page={page - 1}&limit={pageSize}" : null
                },
                data = pagedCharacters
            };
        }

        public async Task<StarWarsCharacter> GetCharacterByIdAsync(string id)
        {
            var cacheKey = $"character_{id}";
            
            if (_cache.TryGetValue(cacheKey, out StarWarsCharacter cachedCharacter))
            {
                return cachedCharacter;
            }

            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/characters/{id}");
                
                // If we get a 500 error from the external API, treat it as a not found
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return null;
                }
                
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var character = JsonSerializer.Deserialize<StarWarsCharacter>(content);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(cacheKey, character, cacheOptions);

                return character;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}