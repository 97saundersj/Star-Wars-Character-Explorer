using StarWarsCharactersWebAPI.Models;
using StarWarsCharactersWebAPI.Services.Interfaces;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterCacheService _cacheService;
        private readonly ICharacterPaginationService _paginationService;
        private readonly ISearchService<Character> _searchService;

        public CharacterService(
            ICharacterCacheService cacheService,
            ICharacterPaginationService paginationService,
            ISearchService<Character> searchService)
        {
            _cacheService = cacheService;
            _paginationService = paginationService;
            _searchService = searchService;
        }

        public async Task<CharacterResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string? search = null)
        {
            var allCharacters = await _cacheService.GetAllCharactersAsync();
            var filteredCharacters = _searchService.Search(search ?? string.Empty, allCharacters);

            return _paginationService.CreatePaginatedResult(
                filteredCharacters,
                page,
                limit ?? 10);
        }

        public async Task<Character> GetCharacterByIdAsync(string id)
        {
            var allCharacters = await _cacheService.GetAllCharactersAsync();
            return allCharacters.First(c => c.Id == id);
        }
    }
}