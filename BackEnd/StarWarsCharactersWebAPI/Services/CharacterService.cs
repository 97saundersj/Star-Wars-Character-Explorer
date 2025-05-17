using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Models;
using System.Text;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterService(ICharacterCacheService cacheService) : ICharacterService
    {
        private readonly ICharacterCacheService _cacheService = cacheService;

        public async Task<StarWarsResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string? search = null)
        {
            var allCharacters = await _cacheService.GetAllCharactersAsync();
            var filteredCharacters = FilterCharacters(allCharacters.Data, search);

            return CreatePaginatedResponse(filteredCharacters, page, limit ?? 10);
        }

        public Task<StarWarsCharacter> GetCharacterByIdAsync(string id)
        {
            return _cacheService.GetCharacterByIdAsync(id);
        }

        private static List<StarWarsCharacter> FilterCharacters(List<StarWarsCharacter> characters, string? search)
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

        private static StarWarsResponse CreatePaginatedResponse(List<StarWarsCharacter> characters, int page, int pageSize)
        {
            var totalItems = characters.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));

            var pagedCharacters = characters
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new StarWarsResponse
            {
                Info = new()
                {
                    Total = totalItems,
                    Page = page,
                    Limit = pageSize,
                    Next = page < totalPages ? $"/api/StarWars/characters?page={page + 1}&limit={pageSize}" : null,
                    Prev = page > 1 ? $"/api/StarWars/characters?page={page - 1}&limit={pageSize}" : null
                },
                Data = pagedCharacters
            };
        }
    }
}