using System.Text;
using System.Text.Json.Serialization;

namespace Task1
{
    public record SearchResult
    {
        [JsonPropertyName("pageid")]
        public required int PageId { get; init; }

        [JsonPropertyName("title")]
        public required string Title { get; init; }

        [JsonPropertyName("snippet")]
        public required string Snippet { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{"Page ID:", -10}{PageId, -20}");
            sb.AppendLine($"{"Title:", -10}{Title, -20}");
            sb.AppendLine($"{"Snippet:", -10}{Snippet, -20}");

            return sb.ToString();
        }
    };    
}
        