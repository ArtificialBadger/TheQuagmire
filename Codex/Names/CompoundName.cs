using System;
using System.Collections.Generic;
using System.Text;
using static Codex.MyExtensions;

namespace Codex.Names
{
    public class CompoundName : Name
    {
        public string Identifier { get; set; }

        public string Noun { get; set; }

        public override string GetName()
        {
            return $"{this.Identifier.UppercaseFirst()} {this.Noun}";
        }
    }
}
