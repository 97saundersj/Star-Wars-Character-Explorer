using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace StarWarsCharactersWebAPI.Services
{
    public class SearchService<T>(IMemoryCache cache) : ISearchService<T> where T : class, IStarWarsEntity
    {
        private readonly IMemoryCache _cache = cache;
        private const string _normalizedDataCacheKey = "normalized_entity_data";
        private const int _defaultCacheExpirationMinutes = 15;

        public IEnumerable<T> Search(string searchTerm, IEnumerable<T> items)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return items;

            var normalizedSearch = NormalizeString(searchTerm);
            var normalizedData = GetOrCreateNormalizedData(items);

            return items
                .Where(entity => 
                {
                    var (normalizedName, normalizedDescription) = normalizedData[entity];
                    return normalizedName.Contains(normalizedSearch) || 
                           normalizedDescription.Contains(normalizedSearch);
                })
                .OrderBy(entity => !normalizedData[entity].NormalizedName.Contains(normalizedSearch))
                .ToList();
        }

        private Dictionary<T, (string NormalizedName, string NormalizedDescription)> GetOrCreateNormalizedData(IEnumerable<T> items)
        {
            if (_cache.TryGetValue(_normalizedDataCacheKey, out Dictionary<T, (string, string)>? cachedData))
            {
                return cachedData;
            }

            var normalizedData = new Dictionary<T, (string, string)>();
            foreach (var item in items)
            {
                normalizedData[item] = (
                    NormalizeString(item.Name),
                    item.Description != null ? NormalizeString(item.Description) : string.Empty
                );
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(_defaultCacheExpirationMinutes));
            _cache.Set(_normalizedDataCacheKey, normalizedData, cacheOptions);

            return normalizedData;
        }

        private static string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return input
                .ToLowerInvariant()
                .Replace("-", "")
                .Replace(" ", "");
        }
    }
} 