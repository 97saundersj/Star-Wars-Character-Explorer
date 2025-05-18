using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public class PaginationInfo(int total, int page, int limit, string? next = null, string? prev = null)
    {
        [JsonPropertyName("total")]
        public int Total { get; set; } = total;

        [JsonPropertyName("page")]
        public int Page { get; set; } = page;

        [JsonPropertyName("limit")]
        public int Limit { get; set; } = limit;

        [JsonPropertyName("next")]
        public string? Next { get; set; } = next;

        [JsonPropertyName("prev")]
        public string? Prev { get; set; } = prev;
    }
}