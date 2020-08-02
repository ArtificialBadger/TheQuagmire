using Codex.Languages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Codex.WordRetrieval
{
    public class WordRetriever : IWordRetriever
    {
        public async Task<IEnumerable<string>> GetWords(string seed)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var words = new List<string>();

            var seedFileName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.Contains($".{seed}."));

            if (seedFileName == null)
            {
                return words;
            }

            using var stream = assembly.GetManifestResourceStream(seedFileName);
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                words.Add(await reader.ReadLineAsync());
            }

            words = words.Select(s => s.Trim()).Where(s => !String.IsNullOrWhiteSpace(s) && !s.StartsWith("--")).Distinct().ToList();
            
            return words;
        }
    }
}
