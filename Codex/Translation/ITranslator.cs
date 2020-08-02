using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.Translation
{
    public interface ITranslator
    {
        public string Translate(string word);
    }
}
