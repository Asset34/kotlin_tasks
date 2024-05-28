using System.Text;

namespace Task2
{
    public class App
    {
        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.InputEncoding = Encoding.Unicode;
                    Console.OutputEncoding = Encoding.Unicode;

                    // Read input
                    Console.WriteLine("Enter file path (or 'quit' to exit): ");
                    Console.Write("-> ");
                    string input = Console.ReadLine();
                    Console.WriteLine();

                    // Process input
                    if (input == "quit")
                    {
                        break;
                    }

                    // Process path
                    Console.WriteLine("Analyzing...");
                    CityDataAnalyzer analyzer = new();
                    SummaryStatistic stats = analyzer.Analyze(input);

                    // Output results
                    Console.WriteLine();
                    Console.WriteLine(stats.ToString());
                    Console.WriteLine();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Error: File does not exist");
                    Console.WriteLine();
                }
                catch (NotSupportedException e)
                {
                    Console.WriteLine("Error: Format " + '"' + e.Message + '"' + " is not supported");
                    Console.WriteLine();
                }
                catch (InvalidDataException)
                {
                    Console.WriteLine("Error: Bad data");
                    Console.WriteLine();
                }
            }
        }
    }
}