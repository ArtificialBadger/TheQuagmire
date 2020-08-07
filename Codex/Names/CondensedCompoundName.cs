using System;
using System.Collections.Generic;
using System.Text;
using static Codex.MyExtensions;

namespace Codex.Names
{
    public class CondensedCompoundName : Name
    {
        public string Identifier { get; set; }
        public string Noun { get; set; }

        public CondensedCompoundName() { }

        public CondensedCompoundName(CompoundName compoundName)
        {
            this.Identifier = compoundName.Identifier;
            this.Noun = compoundName.Noun;
        }

        public CondensedCompoundName(CondensedCompoundName compoundName)
        {
            this.Identifier = compoundName.Identifier;
            this.Noun = compoundName.Noun;
        }

        public override string GetName()
        {
            return $"{this.Identifier.UppercaseFirst()}{this.Noun.ToString()}";
        }
    }
}
