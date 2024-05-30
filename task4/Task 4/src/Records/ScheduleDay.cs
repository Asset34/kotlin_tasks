using System.Text;
using System.Text.Json.Serialization;

namespace Task_4
{
    public record ScheduleDay
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        [JsonPropertyName("lessons")]
        public required List<Subject> Subjects { get; set; } = [];

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine(">>>>> " + Name);
            foreach (var subject in Subjects)
            {
                sb.AppendLine(new string('-', 20));
                sb.AppendLine(subject.ToString());
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}