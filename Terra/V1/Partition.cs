using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public struct Partition
    {
        public List<Partition> Partitions { get; set; }

        public decimal Share { get; set; }

        public string Name { get; set; }

        public Partition(Partition partition)
        {
            Partitions = partition.Partitions;
            Share = partition.Share;
            Name = partition.Name;
        }
    }
}
