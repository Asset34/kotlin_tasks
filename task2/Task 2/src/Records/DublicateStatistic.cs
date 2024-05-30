using System.Text;

namespace Task2
{
    public record DublicateStatistic
    {
        public required CityDataRecord Record { get; init; }
        public required int Dublicates { get; init; }

        public override string? ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"{"Record:", -20}{Record.ToString(), -40}");
            sb.AppendLine($"{"Dublicates:", -20}{Dublicates, -40}");

            return sb.ToString();
        }
    }
}
