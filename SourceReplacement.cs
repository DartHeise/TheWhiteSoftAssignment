namespace TheWhiteSoftAssignment
{
    internal class SourceReplacement: IComparable
    {
        public string replacement { get; set; }
        public string source { get; set; }
        public SourceReplacement(string replacement, string source)
        {
            this.replacement = replacement;
            this.source = source;
        }

        public int CompareTo(object? obj) // Сортировка по длине ключа
        {
            SourceReplacement anotherReplacement = (SourceReplacement)obj;
            if (replacement.Length > anotherReplacement.replacement.Length)
                return -1;
            else if (replacement.Length < anotherReplacement.replacement.Length)
                return 1;

            return 0;
        }
    }
}
