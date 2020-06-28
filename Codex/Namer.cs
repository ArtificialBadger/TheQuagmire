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

            return DisplayPossibilities(word1, word2, "Best");
        }

        private string DisplayPossibilities(string a, string b, string title = "")
        {
            if (a.Contains("'") || b.Length > 6 || Random.Next(0, 2) == 0)
            {
                return $"{a} {b}";
            }
            else
            {
                return $"{a}{b.ToLower()}";
            }
        }
    }
}
