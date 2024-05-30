using System.Text.Json;
using System.Web;

namespace Task1
{
    public static class MediaWikiSearcher
    {
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