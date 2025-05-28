using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ICharacterPaginationService
    {
        /// <summary>
        /// Creates a paginated response for a collection of Star Wars characters.
        /// </summary>
        /// <param name="items">The collection of characters to paginate</param>
        /// <param name="page">The current page number</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="additionalParams">Optional additional query parameters to include in pagination links</param>
        /// <returns>A paginated response containing the characters and pagination information</returns>
        CharacterResponse CreatePaginatedResult(
            IEnumerable<Character> items,
            int page,
            int pageSize);
    }
} 