using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Task2
{
    public class CityDataAnalyzer
    {
        public SummaryStatistic Analyze(string filepath)
        {
            var timer = Stopwatch.StartNew();

            CityData cityData = Deserialize(filepath);

            // Compute statistics
            
            Dictionary<CityDataRecord, int> dublicationAnalyzeTable = [];
            Dictionary<string, int[]> floorsAnalyzeTable = [];

            foreach (var record in cityData.Records)
            {
                if (dublicationAnalyzeTable.TryGetValue(record, out int occurrences))
                {
                    // Update number of dublicates statistics for current record
                    dublicationAnalyzeTable[record] = occurrences + 1;
                }
                else
                {
                    // Add 1st occurrence of current record
                    dublicationAnalyzeTable.Add(record, 0);

                    // Add 1st occurrence of City of the current record
                    if (floorsAnalyzeTable.TryGetValue(record.City, out int[] floors))
                    {
                        // Update number of floors for City of the current record
                        floorsAnalyzeTable[record.City][record.Floor] = floors[record.Floor] + 1;
                    }
                    else
                    {
                        floors = [0, 0, 0, 0, 0, 0];
                        floors[record.Floor] = 1;

                        floorsAnalyzeTable.Add(record.City, floors);
                    }

                }
            }

            // Compile statistics data

            var dublicateStatistics = 
                from dublicate in dublicationAnalyzeTable
                where dublicate.Value > 0
                select new DublicateStatistic { Record = dublicate.Key, Dublicates = dublicate.Value };

            var floorsStatistics =
                from cityFloors in floorsAnalyzeTable
                select new FloorsStatistic { Name = cityFloors.Key, Floors = cityFloors.Value };

            timer.Stop();

            return new SummaryStatistic {
                DublicateStatistics = dublicateStatistics.ToList(),
                FloorsStatistics = floorsStatistics.ToList(),
                AnalysisTime = timer.ElapsedMilliseconds
            };
        }

        private CityData Deserialize(string filepath)
        {
            // Determine file type
            string ext = Path.GetExtension(filepath);

            return ext switch
            {
                ".xml" => DeserializeXML(filepath),
                ".csv" => DeserializeCSV(filepath),
                _ => throw new NotSupportedException(ext)
            };
        }

        private CityData DeserializeXML(string filepath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(CityData));
                using var fileStream = new FileStream(filepath, FileMode.Open);

                return (CityData) serializer.Deserialize(fileStream);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidDataException();
            }
        }

        private CityData DeserializeCSV(string filepath)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";", Encoding = Encoding.UTF8 };
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<CityDataRecord>().ToList();

                    return new CityData() { Records = records };
                };
            }
            catch (CsvHelperException)
            {
                throw new InvalidDataException();
            }
        }
    }
}