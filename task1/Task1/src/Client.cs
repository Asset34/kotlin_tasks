using System.Diagnostics;
using System.Text;

namespace Task1
{
    public class Client
    {
        public static void Run()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            // Read search string
            string? searchString;
            do
            {
                Console.Write("-> Enter search request: ");
                searchString = Console.ReadLine();
            }
            while (String.IsNullOrWhiteSpace(searchString));
            Console.WriteLine();
            

            // Make a search request
            var task = MediaWikiSearcher.SearchAsync(searchString);
            task.Wait();
            List<SearchResult>? searchResults = task.Result;

            // Process response
            if (searchResults is not null)
            {
                // Output results
                if (searchResults.Any())
                {
                    Console.WriteLine("-> Results:");

                    int k = 0;
                    foreach(var entry in searchResults)
                    {
                        // Header
                        Console.WriteLine("-| " + k + " |" + new string('-', 50));

                        // Content
                        Console.WriteLine(entry.ToString());

                        k++;
                    }

                    // Select
                    int selected;
                    string? selectedString;
                    bool parseCheck;
                    bool bordersCheck;
                    do
                    {
                        Console.Write("-> Select: ");
                        selectedString = Console.ReadLine();

                        parseCheck = int.TryParse(selectedString, out selected);
                        bordersCheck = (selected >= 0) && (selected < searchResults.Count);
                    }
                    while (!parseCheck || !bordersCheck);

                    // Open selected page
                    string url = MediaWikiSearcher.GetUrl(searchResults[selected].PageId);
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else
                {
                    Console.WriteLine("-> No Results");
                }
            }

        }
    }
}