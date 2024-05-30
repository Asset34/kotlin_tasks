using System.Text;

namespace Task2
{
    public record FloorsStatistic
    {
        public required string Name { get; init; }
        public int[] Floors { get; set; } = new int[6]; // [0] - unused, [i] - ith floor

        public override string? ToString()
        {
            StringBuilder sb = new();

            sb.Append($"{Name, -20}|");

            for (int i = 1; i < 6; i++)
            {
                sb.Append($"{Floors[i], -10}");
            }

            return sb.ToString();
        }
    }
}
