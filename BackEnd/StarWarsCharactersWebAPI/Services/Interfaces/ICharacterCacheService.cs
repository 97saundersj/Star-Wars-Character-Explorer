using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ICharacterCacheService
    {
        Task<StarWarsResponse> GetAllCharactersAsync();
        Task<StarWarsCharacter> GetCharacterByIdAsync(string id);
    }
}