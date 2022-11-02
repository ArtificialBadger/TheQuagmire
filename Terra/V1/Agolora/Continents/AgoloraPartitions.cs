using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1;

namespace Terra.V1.Agolora.Continents
{
    static class AgoloraPartitions
    {
        public static List<Partition> Continents = new List<Partition>()
        {
            new Partition() { Name = "West Toth", Share = .075m, Partitions = West_Toth.WestTothPartitions.Partitions }, // .025
	        new Partition() { Name = "Toth", Share = .06m },
            new Partition() { Name = "Omr", Share = .239m, Partitions = Omr.OmrPartitions.Partitions},
            new Partition() { Name = "Asnil Renders", Share = .0025m },
            new Partition() { Name = "Ferro-Ajjre", Share = .1925m },
            new Partition() { Name = "Amber Kingdom", Share = .17m },
            new Partition() { Name = "Vresier", Share = .16m, Partitions = Vresier.VersierPartitions.Partitions},
            new Partition() { Name = "Fedress", Share = .05m },
            new Partition() { Name = "Yuyal", Share = .05m },
            new Partition() { Name = "Lesser Ending", Share = .001m },
        };
    }
}
