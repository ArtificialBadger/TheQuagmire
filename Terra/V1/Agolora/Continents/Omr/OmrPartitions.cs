using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1;

namespace Terra.V1.Agolora.Continents.Omr
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
