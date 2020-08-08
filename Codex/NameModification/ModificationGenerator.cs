using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Codex.NameModification
{
    public class ModificationGenerator
    {
        private Random random = new Random();

        private LanguageGenerator languageGenerator;

        public ModificationGenerator(LanguageGenerator languageGenerator)
        {
            this.languageGenerator = languageGenerator;
        }

        public async Task<IEnumerable<INameModifier>> GetNameModifiers()
        {
            var nameModifiers = new List<INameModifier>();

            var iterations = this.random.Next(0, 10);

            for (int i = 0; i < iterations; i++)
            {
                var modifierIndex = random.Next(0, 5);

                if (modifierIndex == 0)
                {
                    nameModifiers.Add(new ConcatModifier());
                }
                else if (modifierIndex == 1)
                {
                    nameModifiers.Add(new SwapModifier());
                }
                else if (modifierIndex == 2)
                {
                    nameModifiers.Add(new TranslateModifier() { Language = await languageGenerator.Generate("Words", "Visis") });
                }
            }

            return nameModifiers;
        }
    }
}
