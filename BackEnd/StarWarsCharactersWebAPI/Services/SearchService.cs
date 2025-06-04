using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services
{
    public class SearchService<T> : ISearchService<T> where T : class
    {
        public IEnumerable<T> Search(string searchTerm, IEnumerable<T> items)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return items;

            var normalizedSearch = NormalizeString(searchTerm);

            return items
                .Where(item => 
                {
                    if (item is IStarWarsEntity entity)
                    {
                        return NormalizeString(entity.Name).Contains(normalizedSearch) ||
                               (entity.Description != null && NormalizeString(entity.Description).Contains(normalizedSearch));
                    }
                    return false;
                })
                .OrderBy(item => 
                {
                    if (item is IStarWarsEntity entity)
                    {
                        return !NormalizeString(entity.Name).Contains(normalizedSearch);
                    }
                    return true;
                })
                .ToList();
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