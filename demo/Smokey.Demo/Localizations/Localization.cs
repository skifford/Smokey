using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Smokey.Demo.Localizations
{
    public static class Localization
    {
        private static readonly Dictionary<Language, LocalizationModel> Cache = new();
        private static readonly Dictionary<Language, string> FileByLanguage = new()
        {
            { Language.Ru, "Localizations/ru.json" }
        };

        public static LocalizationModel For(Language language)
        {
            if (Cache.TryGetValue(language, out var result))
            {
                return result;
            }
            
            var json = File.OpenText(FileByLanguage[language]).ReadToEnd();
            var localization = JsonSerializer.Deserialize<LocalizationModel>(json);
            
            Cache[language] = localization;

            return localization;
        }
    }
}
