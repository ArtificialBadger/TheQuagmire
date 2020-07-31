using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Codex
{
    public class Namer
    {
        private static Random Random = new Random();

        public string GetName()
        {
            var text = System.IO.File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Words.txt");
            var descriptorText = System.IO.File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Descriptors.txt");

            var words = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(w => w.Trim()).Where(w => !String.IsNullOrWhiteSpace(w) && !w.StartsWith("--")).Distinct().ToList();
            var descriptors = descriptorText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(w => w.Trim()).Where(w => !String.IsNullOrWhiteSpace(w) && !w.StartsWith("--")).Distinct().ToList();

            var word1 = descriptors.Pick();
            var word2 = words.Pick();
            var word3 = words.Pick();

            return DisplayPossibilities(word1, word2, word3);
        }

        private string DisplayPossibilities(string a, string b, string c)
        {
            var rand = Random.Next(0, 7);

            var connectors = new List<string>() { "under the", "in the", "of the", "over the", "near the", "once" };

            var connector = connectors[Random.Next(0, connectors.Count)];

            if (a.Contains("'") || a.EndsWith("ed") || b.Length > 6 || rand == 0)
            {
                return $"{a} {b}";
            }
            else if (rand == 1)
            {
                return $"{b} of {a}";
            }
            else if (rand == 2 && !String.IsNullOrWhiteSpace(c))
            {
                return $"{b} of {a}{c.ToLower()}";
            }
            else if (rand > 3  && rand < 5 && !String.IsNullOrWhiteSpace(c))
            {
                return $"{b} {connector} {a} {c}";
            }
            else
            {
                return $"{a}{b.ToLower()}";
            }
        }
    }
}
