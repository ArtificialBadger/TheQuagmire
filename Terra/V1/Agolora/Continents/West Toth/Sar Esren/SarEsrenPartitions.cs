using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1;

namespace Terra.V1.Agolora.Continents.West_Toth.Sar_Esren
{
    public static class SarEsrenPartitions
    {
        public static List<Partition> Partitions = new List<Partition>()
        {
            new Partition() { Name = "Upper Sar Esren", Share = .1m },
            new Partition() { Name = "Border Cities", Share = .8m },
            new Partition() { Name = "Other", Share = .1m },
        };
    }
}
