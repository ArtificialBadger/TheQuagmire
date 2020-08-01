using Codex.Names;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codex.NameModification
{
    public interface INameModifier
    {
        public Name Modify(Name name);
    }
}
