using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ICharacterService
    {
        /// <summary>
        /// Retrieves a paginated list of Star Wars characters with optional search filtering.
        /// </summary>
        /// <param name="page">The page number to retrieve (default: 1)</param>
        /// <param name="limit">The maximum number of characters per page (default: null)</param>
        /// <param name="search">Optional search term to filter characters by name</param>
        /// <returns>A paginated response containing the list of characters and pagination information</returns>
        Task<CharacterResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string? search = null);

        /// <summary>
        /// Retrieves a specific Star Wars character by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the character to retrieve</param>
        /// <returns>The character if found, null otherwise</returns>
        Task<Character> GetCharacterByIdAsync(string id);
    }
}