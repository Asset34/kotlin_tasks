using System.Xml.Serialization;

namespace Task2
{
    [XmlRoot("root")]
    public record CityData
    {
        [XmlElement("item")]
        public required List<CityDataRecord> Records { get; set; }
    }
}