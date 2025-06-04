using StarWarsCharactersWebAPI.Models;

namespace StarWarsCharactersWebAPI.Services.Interfaces
{
    public interface ISearchService<T> where T : class
    {
        IEnumerable<T> Search(string searchTerm, IEnumerable<T> items);
    }
} 