using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.Names
{
    public sealed class DualNounName : Name
    {
        public string FirstNoun { get; set; }
        
        public string SecondNoun { get; set; }

        public override string GetName()
        {
            return FirstNoun.UppercaseFirst() + " " + SecondNoun.UppercaseFirst();
        }
    }
}
