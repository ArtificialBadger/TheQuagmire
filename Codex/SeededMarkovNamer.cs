using Markov;
using Microsoft.VisualStudio.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Codex
{
    public class SeededMarkovNamer
    {
        private static IDictionary<String, MarkovChain<Char>> fileSeeds = new Dictionary<String, MarkovChain<Char>>();

        private static SemaphoreSlim seedLock = new SemaphoreSlim(1, 1);

        public int Order { get; set; } = 2;

        public async Task LoadSeeds(string fileName, bool forceLoad = false)
        {
            if (forceLoad || !fileSeeds.ContainsKey(fileName))
            {
                await seedLock.WaitAsync();
                try
                {
                    if (forceLoad || !fileSeeds.ContainsKey(fileName))
                    {
                        var chain = new MarkovChain<Char>(Order);
                        var seeds = await this.ReadSeedsFromFile(fileName);
                        foreach (var seed in seeds)
                        {
                            chain.Add(seed);
                        }
                        fileSeeds[fileName] = chain;
                    }
                }
                finally
                {
                    seedLock.Release();
                }
            }
        }

        private async Task<List<String>> ReadSeedsFromFile(string seedName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var seeds = new List<String>();

            var seedFileName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.Contains($".{seedName}."));

            if (seedFileName == null)
            {
                return seeds;
            }

            using var stream = assembly.GetManifestResourceStream(seedFileName);
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                seeds.Add(await reader.ReadLineAsync());
            }

            seeds = seeds.Select(s => s.Trim()).Where(s => !String.IsNullOrWhiteSpace(s) && !s.StartsWith("--")).Distinct().ToList();

            return seeds;
        }

        public async Task<string> GetName(string seedName)
        {
            if (!fileSeeds.ContainsKey(seedName))
            {
                await LoadSeeds(seedName);
            }
            var chain = fileSeeds[seedName];
            return new String(chain.Chain().ToArray()).ToString();
        }
    }
}
