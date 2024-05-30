using System.Text;
using System.Text.Json.Serialization;

namespace Task_4
{

    public record ScheduleWeek
    {
        [JsonPropertyName("week")]
        public required int Week { get; init; }

        [JsonPropertyName("days")]
        public required List<ScheduleDay> DaySchedules { get; init; } = [];

        public override string ToString()
        {
            StringBuilder sb = new();

            foreach (var daySchedule in DaySchedules)
            {
                sb.AppendLine(daySchedule.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}