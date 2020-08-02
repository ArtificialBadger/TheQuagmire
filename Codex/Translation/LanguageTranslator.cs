using Codex.Languages;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Codex.Translation
{
    public class LanguageTranslator : ITranslator
    {
        public Language Language { get; set; }

        public string Translate(string word)
        {
            return this.Language.Lookup[word];
        }
    }
}
