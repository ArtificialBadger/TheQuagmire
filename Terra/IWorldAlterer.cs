using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public interface IWorldAlterer
    {
        int Priority { get; } 

        void Alter(World world);
    }
}
