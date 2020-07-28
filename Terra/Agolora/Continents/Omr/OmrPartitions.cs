using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.Agolora.Continents.Omr
{
    public static class OmrPartitions
    {
        public static List<Partition> Partitions = new List<Partition>()
        {
            new Partition() { Name = "Lis Malos", Share = .8m },
            new Partition() { Name = "Other", Share = .2m }
        };
    }
}
