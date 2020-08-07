
using Codex.Names;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.NameModification
{
    public class SwapModifier : INameModifier
    {
        public Name Modify(Name name)
        {
            switch (name)
            {
                case CompoundName compound:
                    return new CompoundName() { Identifier = compound.Noun, Noun = compound.Identifier };
                case DualNounName dualNoun:
                    return new DualNounName() { FirstNoun = dualNoun.SecondNoun, SecondNoun = dualNoun.FirstNoun };
                case CondensedDualNounName condensedDualNoun:
                    return new CondensedDualNounName() { FirstNoun = condensedDualNoun.SecondNoun, SecondNoun = condensedDualNoun.FirstNoun };
                case CondensedCompoundName condensedCompound:
                    return new CondensedCompoundName() { Identifier = condensedCompound.Noun, Noun = condensedCompound.Identifier };
                case SimpleName simple:
                    return new SimpleName(simple);
                default:
                    return name;
            }
        }
    }
}
