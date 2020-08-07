using Codex.Languages;
using Codex.Names;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Codex.NameModification
{
    public sealed class GreedyTranslateModifier : INameModifier
    {
        public Language Language { get; set; }

        private String Translate(string native)
        {
            foreach (var (a, b) in Language.Lookup)
            {
                return native.Replace(a, b);
            }

            return native;
        }

        public Name Modify(Name name)
        {
            switch (name)
            {
                case CompoundName compound:
                    return new CompoundName() { Identifier = Translate(compound.Identifier), Noun = Translate(compound.Noun) };
                case DualNounName dualNoun:
                    return new DualNounName() { FirstNoun = Translate(dualNoun.FirstNoun), SecondNoun = Translate(dualNoun.SecondNoun) };
                case CondensedDualNounName condensedDualNoun:
                    return new CondensedDualNounName() { FirstNoun = Translate(condensedDualNoun.FirstNoun), SecondNoun = Translate(condensedDualNoun.SecondNoun) };
                case CondensedCompoundName condensedCompound:
                    return new CondensedCompoundName() { Identifier = Translate(condensedCompound.Identifier), Noun = Translate(condensedCompound.Noun) };
                case SimpleName simple:
                    return new SimpleName(simple);
                default:
                    return name;
            }
        }
    }
}
