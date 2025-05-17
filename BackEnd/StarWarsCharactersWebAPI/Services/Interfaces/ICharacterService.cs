using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<StarWarsResponse> GetAllCharactersAsync(int page = 1, int? limit = null, string? search = null);
        Task<StarWarsCharacter> GetCharacterByIdAsync(string id);
    }
}