using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IO.Swagger.Services
{
    /// <summary>
    /// DictionaryService
    /// </summary>
    public static class DictionaryService
    {
        /// <summary>
        /// Dictionary for words
        /// </summary>
        private static List<string> Dictionary = new List<string>();

        /// <summary>
        /// InitDictionary
        /// </summary>
        /// <returns></returns>
        public async static Task InitDictionary()
        {
            var dictionaryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "german-words.json");
            Dictionary = JsonConvert.DeserializeObject<List<string>>(await File.ReadAllTextAsync(dictionaryPath));
        }

        /// <summary>
        /// check if word is in the dictionary
        /// </summary>
        /// <returns></returns>
        public static bool IsWordInDictionary(string word)
        {
            return Dictionary.Contains(word);
        }
    }
}
