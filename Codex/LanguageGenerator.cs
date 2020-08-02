using Codex.Languages;
using Codex.WordRetrieval;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Codex
{
    public class LanguageGenerator
    {
        private readonly SeededMarkovNamer namer;
        private readonly IWordRetriever wordRetriever;

        public LanguageGenerator(SeededMarkovNamer namer, IWordRetriever wordRetriever)
        {
            this.namer = namer;
            this.wordRetriever = wordRetriever;
        }

        public async Task<Language> Generate(string seedName)
        {
            var language = new Language() { Lookup = new Dictionary<string, string>() };
            var wordsNeedingTranslation = await this.wordRetriever.GetWords(seedName);
            
            foreach (var word in wordsNeedingTranslation)
            {
                language.Lookup[word] = await namer.GetName(seedName);
            }

            return language;
        }
    }
}
