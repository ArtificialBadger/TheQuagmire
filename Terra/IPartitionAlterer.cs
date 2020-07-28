using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public interface IPartitionAlterer
    {
        List<Partition> Randomize(List<Partition> partitions, int minPercentage, int maxPercentage, int cycles, int seed = 0);
    }
}
