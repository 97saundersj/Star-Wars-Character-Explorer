using StarWarsCharactersWebAPI.Models;
using StarWarsCharactersWebAPI.Services.Interfaces;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterPaginationService : ICharacterPaginationService
    {
        private const int MaxPageSize = 1000;
        public const string CharactersEndpoint = "/api/StarWars/characters";

        public CharacterResponse CreatePaginatedResult(
            IEnumerable<Character> items,
            int page,
            int pageSize,
            IDictionary<string, string>? additionalParams = null)
        {
            var itemsList = items.ToList();
            var totalItems = itemsList.Count;

            // Ensure page size is within valid bounds (1 to MaxPageSize)
            var validatedPageSize = Math.Clamp(pageSize, 1, MaxPageSize);

            // Ensure requested page number is within valid range
            var validatedPage = Math.Clamp(page, 1, (int)Math.Ceiling(totalItems / (double)validatedPageSize));

            // Get paginated items
            var pagedItems = itemsList
                .Skip((validatedPage - 1) * validatedPageSize)
                .Take(validatedPageSize)
                .ToList();

            var paginationInfo = new PaginationInfo(
                total: totalItems,
                page: validatedPage,
                limit: validatedPageSize
            );

            return new CharacterResponse(
                info: paginationInfo,
                data: pagedItems
            );
        }
    }
}