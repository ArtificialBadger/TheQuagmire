using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public interface IWorldAlterer
    {
        int Priority { get; }

        void Alter(World world);
    }
}
