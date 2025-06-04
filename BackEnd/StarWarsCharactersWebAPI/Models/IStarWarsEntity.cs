using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public interface IStarWarsEntity
    {
        [JsonPropertyName("_id")]
        string Id { get; set; }

        [JsonPropertyName("name")]
        string Name { get; set; }

        [JsonPropertyName("description")]
        string? Description { get; set; }

        [JsonPropertyName("image")]
        string? Image { get; set; }
    }
} 