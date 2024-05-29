using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Task1
{
    public static class MediaWikiSearcher
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
        
        private static readonly HttpClient _httpClient = new();

        private const string _queryUrlBase = "https://ru.wikipedia.org/w/api.php?action=query&list=search&utf8=&format=json&srsearch=";
        private const string _pageUrlBase = "https://ru.wikipedia.org/w/index.php?curid=";


        public static async Task<List<SearchResult>?> SearchAsync(string searchString)
        {
            // Make a request
            string fullRequest = _queryUrlBase + HttpUtility.UrlEncode('"' + searchString + '"');
            var jsonString = await _httpClient.GetStringAsync(fullRequest);

            // Parse response JSON
            var jsonSearchPropertyString = JsonDocument.Parse(jsonString).RootElement.GetProperty("query").GetProperty("search");
            var results = JsonSerializer.Deserialize<List<SearchResult>>(jsonSearchPropertyString);

            return results;
        }

        public static string GetUrl(int pageId)
        {
            return _pageUrlBase + pageId;
        }

    }
}