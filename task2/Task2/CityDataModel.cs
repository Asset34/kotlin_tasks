using System.Text;
using System.Xml.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Task2
{
    [XmlRoot("root")]
    public record CityData
    {
        [XmlElement("item")]
        public required List<CityDataRecord> Records { get; set; }
    }

    public record CityDataRecord
    {
        [Name("city")]
        [XmlAttribute("city")]
        public required string City { get; init; }

        [Name("street")]
        [XmlAttribute("street")]
        public required string Street { get; init; }

        [Name("house")]
        [XmlAttribute("house")]
        public required int House { get; init; }

        [Name("floor")]
        [XmlAttribute("floor")]
        public required int Floor { get; init; }

        public override string? ToString()
        {
            StringBuilder sb = new();
            
            sb.Append(City + ", ");
            sb.Append(Street + ", ");
            sb.Append(House + ", ");
            sb.Append(Floor);

            return sb.ToString();
        }
    }
}