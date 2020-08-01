using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.Names
{
    public class SimpleName : Name
    {
        public String Name { get; set; }

        public SimpleName(string name)
        {
            this.Name = name;
        }

        public SimpleName(SimpleName simpleName)
        {
            this.Name = simpleName.Name;
        }

        public override string GetName()
        {
            return Name;
        }
    }
}
