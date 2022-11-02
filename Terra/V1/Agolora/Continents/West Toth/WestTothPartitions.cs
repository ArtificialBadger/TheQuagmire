using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1.Agolora.Continents.West_Toth.Sar_Esren;
using Terra.V1;

namespace Terra.V1.Agolora.Continents.West_Toth
{
    public static class WestTothPartitions
    {
        public static List<Partition> Partitions = new List<Partition>()
        {
            new Partition() { Name = "Sar Esren", Share = .2m, Partitions = SarEsrenPartitions.Partitions },
            new Partition() { Name = "Cloudlakes", Share = .2m },
            new Partition() { Name = "Other", Share = .6m },
        };
    }
}
