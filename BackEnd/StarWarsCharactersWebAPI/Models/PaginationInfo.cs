using System.Text.Json.Serialization;

namespace StarWarsCharactersWebAPI.Models
{
    public class PaginationInfo(int total, int page, int limit)
    {
        [JsonPropertyName("total")]
        public int Total { get; } = total;

        [JsonPropertyName("page")]
        public int Page { get; } = page;

        [JsonPropertyName("limit")]
        public int Limit { get; } = limit;

        [JsonPropertyName("hasNext")]
        public bool HasNext => Page < (int)Math.Ceiling(Total / (double)Limit);

        [JsonPropertyName("hasPrevious")]
        public bool HasPrevious => Page > 1;
    }
}