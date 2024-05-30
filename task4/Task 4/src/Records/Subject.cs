using System.Text;
using System.Text.Json.Serialization;

namespace Task_4
{
    public record Subject
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("subjectType")]
        public required string Type { get; init; }

        [JsonPropertyName("week")]
        public required string Week { get; init; }

        [JsonPropertyName("teacher")]
        public required string Teacher { get; init; }

        [JsonPropertyName("start_time")]
        public required string StartTime { get; init; }

        [JsonPropertyName("end_time")]
        public required string EndTime { get; init; }
        
        [JsonPropertyName("room")]
        public required string Room { get; init; }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{"Name:",       -14}{Name,      -40}");
            sb.AppendLine();
            sb.AppendLine($"{"Type:",       -14}{Type,      -40}");
            sb.AppendLine($"{"Teacher:",    -14}{Teacher,   -40}");
            sb.AppendLine($"{"Start Time:", -14}{StartTime, -40}");
            sb.AppendLine($"{"Week:",       -14}{Week,      -40}");
            sb.AppendLine($"{"End Time:",   -14}{EndTime,   -40}");
            sb.AppendLine($"{"Room:",       -14}{Room,      -40}");


            return sb.ToString();
        }

        // System properties

        [JsonPropertyName("start_time_seconds")]
        public required long StartTimeSeconds { get; init; }

        [JsonPropertyName("end_time_seconds")]
        public required long EndTimeSeconds { get; init; }
    }
}