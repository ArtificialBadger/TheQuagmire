using Codex.Languages;
using Codex.Names;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Codex.NameModification
{
    public class TranslateModifier : INameModifier
    {
        public Language Language { get; set; }

        private string Translate (string word)
        {
            return this.Language.Lookup[word] ?? word;
        }

        public Name Modify(Name name)
        {
            switch (name)
            {
                case CompoundName compound:
                    return new CompoundName() { Identifier = Translate(compound.Identifier), Noun = Translate(compound.Noun) };
                case DualNounName dualNoun:
                    return new DualNounName() { FirstNoun = Translate(dualNoun.FirstNoun), SecondNoun = Translate(dualNoun.SecondNoun)};
                case CondensedCompoundName compound:
                    return new CondensedCompoundName() { Identifier = Translate(compound.Identifier), Noun = Translate(compound.Noun) };
                case CondensedDualNounName dualNoun:
                    return new CondensedDualNounName() { FirstNoun = Translate(dualNoun.FirstNoun), SecondNoun = Translate(dualNoun.SecondNoun) };
                case SimpleName simple:
                    return new SimpleName(simple);
                default:
                    return name;
            }
        }
    }
}
