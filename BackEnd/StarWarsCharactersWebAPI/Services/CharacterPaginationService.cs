using Microsoft.AspNetCore.WebUtilities;
using StarWarsCharactersWebAPI.Models;
using StarWarsCharactersWebAPI.Services.Interfaces;

namespace StarWarsCharactersWebAPI.Services
{
    public class CharacterPaginationService : ICharacterPaginationService
    {
        private const int DefaultPageSize = 10;
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

            // Calculate total number of pages needed to display all items
            var itemsPerPage = validatedPageSize;
            var totalPages = (int)Math.Ceiling(totalItems / (double)itemsPerPage);

            // Ensure requested page number is within valid range (1 to totalPages)
            var validatedPage = Math.Clamp(page, 1, totalPages);

            // Get paginated items
            var pagedItems = itemsList
                .Skip((validatedPage - 1) * validatedPageSize)
                .Take(validatedPageSize)
                .ToList();

            // Build base query parameters
            var queryParams = new Dictionary<string, string>
            {
                { "limit", validatedPageSize.ToString() }
            };

            if (additionalParams != null)
            {
                foreach (var param in additionalParams)
                {
                    queryParams[param.Key] = param.Value;
                }
            }

            // Build next and previous page URLs
            var nextPageUrl = validatedPage < totalPages
                ? QueryHelpers.AddQueryString(CharactersEndpoint, queryParams.Concat(new[] { new KeyValuePair<string, string>("page", (validatedPage + 1).ToString()) }))
                : null;

            var prevPageUrl = validatedPage > 1
                ? QueryHelpers.AddQueryString(CharactersEndpoint, queryParams.Concat(new[] { new KeyValuePair<string, string>("page", (validatedPage - 1).ToString()) }))
                : null;

            var paginationInfo = new PaginationInfo(
                total: totalItems,
                page: validatedPage,
                limit: validatedPageSize,
                next: nextPageUrl,
                prev: prevPageUrl
            );

            return new CharacterResponse(
                info: paginationInfo,
                data: pagedItems
            );
        }
    }
}