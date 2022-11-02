using System;
using System.Collections.Generic;
using System.Text;
using Terra.V1;

namespace Terra.V1.Agolora.Continents.Vresier
{
    public static class VersierPartitions
    {
        public static List<Partition> Partitions = new List<Partition>()
        {
            new Partition() { Name = "Vresier", Share = .6m, Partitions = new List<Partition>() {
                new Partition {Name = "Capine", Share = .6m, Partitions = new List<Partition>() {
                    new Partition() { Name = "Aels", Share = .2m },
                    new Partition() { Name = "Bortr", Share = .2m },
                    new Partition() { Name = "Go-Aen", Share = .2m },
                    new Partition() { Name = "Ospis", Share = .2m },
                    new Partition() { Name = "Bortr", Share = .2m }
            }},
                new Partition() {Name = "Other", Share = .4m}
            }},
            new Partition() { Name = "South Vresier", Share = .4m }
};
    }
}
