using System.Text.Json.Serialization;

namespace Task_4
{
    public record SemesterInfo
    {
        [JsonPropertyName("startDate")]
        public required string StartDate { get; init; }
        
        [JsonPropertyName("endDate")]
        public required string EndDate { get; init; }
    }
}