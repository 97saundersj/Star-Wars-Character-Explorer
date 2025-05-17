using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public class StarWarsResponse
    {
        [JsonPropertyName("info")]
        public StarWarsInfo Info { get; set; }

        [JsonPropertyName("data")]
        public List<StarWarsCharacter> Data { get; set; }
    }
}