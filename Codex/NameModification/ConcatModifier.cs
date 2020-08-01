using Codex.Names;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.NameModification
{
    public class ConcatModifier : INameModifier
    {
        public Name Modify(Name name)
        {
            switch (name)
            {
                case CompoundName compound:
                    return new CondensedCompoundName(compound);
                case DualNounName dualNoun:
                    return new CondensedDualNounName(dualNoun);
                case CondensedDualNounName condensedDualNoun:
                    return new CondensedDualNounName(condensedDualNoun);
                case CondensedCompoundName condensedCompound:
                    return new CondensedCompoundName(condensedCompound);
                case SimpleName simple:
                    return new SimpleName(simple);
                default:
                    return name;
            }
        }
    }
}
