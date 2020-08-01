using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.Names
{
    public sealed class CondensedDualNounName : Name
    {
        public string FirstNoun { get; set; }

        public string SecondNoun { get; set; }

        public CondensedDualNounName() { }

        public CondensedDualNounName(DualNounName dualNoun)
        {
            this.FirstNoun = dualNoun.FirstNoun;
            this.SecondNoun = dualNoun.SecondNoun;
        }

        public CondensedDualNounName(CondensedDualNounName dualNoun)
        {
            this.FirstNoun = dualNoun.FirstNoun;
            this.SecondNoun = dualNoun.SecondNoun;
        }

        public override string GetName()
        {
            return FirstNoun + SecondNoun.ToLower();
        }
    }
}
