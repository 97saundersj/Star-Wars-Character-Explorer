using StarWarsCharactersWebAPI.Models;
using StarWarsCharactersWebAPI.Services.Interfaces;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterService(ICharacterCacheService cacheService, ICharacterPaginationService paginationService) : ICharacterService
    {
        private readonly ICharacterCacheService _cacheService = cacheService;
        private readonly ICharacterPaginationService _paginationService = paginationService;

        public async Task<CharacterResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string? search = null)
        {
            var allCharacters = await _cacheService.GetAllCharactersAsync();
            var filteredCharacters = FilterCharacters(allCharacters, search);

            var additionalParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(search))
            {
                additionalParams["search"] = search;
            }

            return _paginationService.CreatePaginatedResult(
                filteredCharacters,
                page,
                limit ?? 10,
                additionalParams);
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            var allCharacters = await _cacheService.GetAllCharactersAsync();
            return allCharacters.First(c => c.Id == id);
        }

        private static List<Character> FilterCharacters(List<Character> characters, string? search)
        {
            if (string.IsNullOrEmpty(search))
                return characters;

            var normalizedSearch = NormalizeString(search);

            return characters
                .Where(c => NormalizeString(c.Name).Contains(normalizedSearch) ||
                          (c.Description != null && NormalizeString(c.Description).Contains(normalizedSearch)))
                .OrderBy(c => !NormalizeString(c.Name).Contains(normalizedSearch))
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