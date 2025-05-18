using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public class CharacterResponse(PaginationInfo info, List<Character> data)
    {
        [JsonPropertyName("info")]
        public PaginationInfo Info { get; set; } = info;

        [JsonPropertyName("data")]
        public List<Character> Data { get; set; } = data;
    }
}