using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public class Character(string id, string name, string? description, string? image)
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = id;

        [JsonPropertyName("name")]
        public string Name { get; set; } = name;

        [JsonPropertyName("description")]
        public string? Description { get; set; } = description;

        [JsonPropertyName("image")]
        public string? Image { get; set; } = image;
    }
}