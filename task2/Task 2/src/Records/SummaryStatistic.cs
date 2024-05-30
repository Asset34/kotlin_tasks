using System.Text;

namespace Task2
{
    public record SummaryStatistic
    {
        public required List<DublicateStatistic> DublicateStatistics { get; init; }
        public required List<FloorsStatistic> FloorsStatistics { get; init; }
        public required long AnalysisTime { get; init; }

        public override string? ToString()
        {
            StringBuilder sb = new();

            // Add Dublicate Statistics
            if (DublicateStatistics.Count != 0)
            {
                sb.AppendLine(">>>>> Dublicate Statistics <<<<<");
                sb.AppendLine();
                
                foreach (var stat in DublicateStatistics)
                {
                    sb.AppendLine(new string('-', 60));
                    sb.AppendLine(stat.ToString());
                }

                sb.AppendLine();
                sb.AppendLine();
            }

            // Add Floors Statistics
            if (FloorsStatistics.Count != 0)
            {
                sb.AppendLine(">>>>> Floors Statistics <<<<<");
                sb.AppendLine();

                sb.Append($"{"City", -20}|");
                for (int i = 1; i < 6; i++)
                {
                    sb.Append($"{"Floor " + i, -10}");
                }
                sb.AppendLine();
                sb.AppendLine(new string('-', 70));

                
                foreach (var stat in FloorsStatistics)
                {
                    sb.AppendLine(stat.ToString());
                }
            }

            // Add Analysis Time info
            sb.AppendLine();
            sb.AppendLine(">>> Analysis Time: " + AnalysisTime + " ms");

            return sb.ToString();
        }
    }
}
