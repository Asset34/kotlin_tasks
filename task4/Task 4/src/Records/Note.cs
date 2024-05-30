using System.Text;

namespace Task_4
{
    public record Note
    {
        public required int Id { get; init; }

        public required DateTime Created { get; init; }
        
        public string Title { get; set; } = "";
        
        public string Content { get; set; } = "";

        public override string ToString()
        {
            return $"{Id, -10}|{Title, -20}|{Content, -40}";
        }
        public string ToStringMeta()
        {
            return $"{$"{Id})", -6}{Created.ToString("MM/dd/yy H:mm"), -20}{Title, -40}";
        }
        public string ToStringContent()
        {
            return $">>> {Title}\n" + Content;
        }
    }
}