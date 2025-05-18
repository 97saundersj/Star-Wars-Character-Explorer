using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ICharacterCacheService
    {
        /// <summary>
        /// Retrieves all Star Wars characters from the cache or fetches them if not cached.
        /// </summary>
        /// <returns>A list of all Star Wars characters</returns>
        Task<List<Character>> GetAllCharactersAsync();
    }
}