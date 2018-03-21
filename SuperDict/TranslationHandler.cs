using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperDict
{
   public static class TranslationHandler
    {
        private static string[] Languages = { "EE", "EN", "RU" };
        private static List<Translation> _translations;

        public static List<Translation> Translations
        {
            set => _translations = value;
            get => _translations ?? (_translations = new List<Translation>());
        }
        public static void AddEntry(string fromWord,
                                    string fromLanguage,
                                    string toWord,
                                    string toLanguage)
        {
            if (fromWord == null)
                throw new ArgumentNullException(nameof(fromWord));
            if (fromLanguage == null)
                throw new ArgumentNullException(nameof(fromLanguage));
            if (toWord == null)
                throw new ArgumentNullException(nameof(toWord));
            if (toLanguage == null)
                throw new ArgumentNullException(nameof(toLanguage));
            if(!Languages.Contains(fromLanguage) || !Languages.Contains(toLanguage))
                throw  new ArgumentException("Invalid language.");
            try
            {
                Translation newItem = new Translation(fromWord, fromLanguage, toWord, toLanguage);

                if (Translations.Any(x => x.FromWord == fromWord && x.FromLanguage == fromLanguage && x.ToLanguage == toLanguage))
                    throw new Exception("Dictionary has translation already.");
                Translations.Add(newItem);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void Remove(string word,
                                   string language)
        {
            if (word == null)
                throw new ArgumentNullException(nameof(word));
            if (language == null)
                throw new ArgumentNullException(nameof(language));

            Translation toRemove = Translations.SingleOrDefault(x => x.FromLanguage == language && x.FromWord == word || x.ToLanguage == language && x.ToWord == word);
            if (toRemove == null)
                throw new Exception("Nothing to delete.");
            Translations.Remove(toRemove);
        }

        public static void Clear()
        {
            Translations.Clear();
        }

        public static string Translate(string fromWord,
                                        string fromLanguage,
                                        string toLanguage)
        {
            if (fromWord == null)
                throw new ArgumentNullException(nameof(fromWord));
            if (fromLanguage == null)
                throw new ArgumentNullException(nameof(fromLanguage));
            if (toLanguage == null)
                throw new ArgumentNullException(nameof(toLanguage));

            string result = Translations.SingleOrDefault(x => x.FromLanguage == fromLanguage && x.FromWord == fromWord && x.ToLanguage == toLanguage)?.ToWord ?? Translations.SingleOrDefault(x => x.ToLanguage == fromLanguage && x.ToWord == fromWord && x.FromLanguage == toLanguage)?.FromWord;
            if (result == null)
                throw new Exception("Not found");
            return result;
        }
    }
}
