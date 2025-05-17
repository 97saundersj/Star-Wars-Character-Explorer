using StarWarsCharactersWebAPI.Services.Interfaces;
using StarWarsCharactersWebAPI.Models;

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

            return characters
                .Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                          (c.Description?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false))
                .OrderBy(c => !c.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                .ToList();
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