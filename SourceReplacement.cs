using System.Text.Json.Serialization;

namespace TheWhiteSoftAssignment
{
    internal class SourceReplacement: IComparable
    {
        [JsonPropertyName("replacement")]
        public string Replacement { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }
        public SourceReplacement(string replacement, string source)
        {
            Replacement = replacement;
            Source = source;
        }

        public int CompareTo(object? obj) // Сортировка по длине ключа
        {
            if (obj is SourceReplacement sourceReplacement)
                return sourceReplacement.Replacement.Length.CompareTo(Replacement.Length);
            else
                throw new ArgumentException("Некорректное значение!");
        }
    }
}
