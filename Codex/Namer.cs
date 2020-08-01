using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace Codex
{
    public class Namer
    {
        private static Random random = new Random();
        private List<string> words = new List<string>();
        private List<string> descriptors = new List<string>();

        public Namer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var wordsFileName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Words.txt"));
            var descriptorsFileName = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Descriptors.txt"));

            using (Stream stream = assembly.GetManifestResourceStream(wordsFileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    words.Add(reader.ReadLine());
                }
            }

            using (Stream stream = assembly.GetManifestResourceStream(descriptorsFileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    descriptors.Add(reader.ReadLine());
                }
            }

            words = words.Select(w => w.Trim()).Where(w => !String.IsNullOrWhiteSpace(w) && !w.StartsWith("--")).Distinct().ToList();
            descriptors = descriptors.Select(w => w.Trim()).Where(w => !String.IsNullOrWhiteSpace(w) && !w.StartsWith("--")).Distinct().ToList();
        }

        public string GetName()
        {
            var x = random.Next(0, 2);

            if (x == 0)
            {
                var word1 = descriptors.Pick();
                var word2 = words.Pick();
                var word3 = words.Pick();
                return DisplayPossibilities(word1, word2, word3);
            }
            else
            {
                var word1 = words.Pick();
                var word2 = words.Pick();
                return Concat(word1, word2);
            }

        }

        private string Concat(string a, string b)
        {
            var x = random.Next(0, 2);

            if (x == 0)
            {
                return a + b.ToLower();
            }
            else
            {
                return a + " " + b;
            }
        }

        private string DisplayPossibilities(string a, string b, string c)
        {
            var rand = random.Next(0, 7);

            var connectors = new List<string>() { "under the", "in the", "of the", "over the", "near the", "once" };

            var connector = connectors[random.Next(0, connectors.Count)];

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
            else if (rand > 3 && rand < 5 && !String.IsNullOrWhiteSpace(c))
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
