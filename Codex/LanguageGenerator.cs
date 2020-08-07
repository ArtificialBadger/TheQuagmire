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

        public async Task<Language> Generate(string fileToTranslate, string languageFile)
        {
            var language = new Language() { Lookup = new Dictionary<string, string>() };
            var wordsNeedingTranslation = await this.wordRetriever.GetWords(fileToTranslate);
            
            foreach (var word in wordsNeedingTranslation)
            {
                var potentialWord = await namer.GetName(languageFile);
                while(potentialWord == word || potentialWord.Length > (word.Length * 2))
                {
                    potentialWord = await namer.GetName(languageFile);
                }
                language.Lookup[word] = potentialWord;
            }

            return language;
        }
    }
}
