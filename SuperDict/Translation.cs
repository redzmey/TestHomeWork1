namespace SuperDict
{
    public class Translation
    {
        public Translation(string fromWord,
                           string fromLanguage,
                           string toWord,
                           string toLanguage)
        {
            FromWord = fromWord;
            FromLanguage = fromLanguage;
            ToWord = toWord;
            ToLanguage = toLanguage;
        }
        public string FromWord { get; set; }
        public string ToWord { get; set; }
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
    }
}