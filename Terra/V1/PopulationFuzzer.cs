using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terra.V1
{
    public class PopulationFuzzer : IPartitionAlterer
    {
        public List<Partition> Randomize(List<Partition> partitions, int minPercentage, int maxPercentage, int cycles, int seed = 0)
        {
            var shiftedPartitions = new List<Partition>(partitions);

            for (int i = 0; i < shiftedPartitions.Count; i++)
            {
                if (shiftedPartitions[i].Partitions != null)
                {
                    shiftedPartitions[i] = new Partition() { Name = shiftedPartitions[i].Name, Share = shiftedPartitions[i].Share, Partitions = Randomize(shiftedPartitions[i].Partitions, minPercentage, maxPercentage, cycles, seed) };
                }
            }

            var random = new Random(seed);

            for (int i = 0; i < cycles; i++)
            {
                var location = random.Next(0, shiftedPartitions.Count);
                var nextLocation = random.Next(0, shiftedPartitions.Count);

                if (location == nextLocation)
                {
                    continue;
                }

                var takeFrom = shiftedPartitions[nextLocation];
                var takeAmount = takeFrom.Share * random.Next(minPercentage, maxPercentage) * .01m;
                shiftedPartitions[location] = new Partition() { Name = shiftedPartitions[location].Name, Share = shiftedPartitions[location].Share + takeAmount, Partitions = shiftedPartitions[location].Partitions };
                shiftedPartitions[nextLocation] = new Partition() { Name = shiftedPartitions[nextLocation].Name, Share = shiftedPartitions[nextLocation].Share - takeAmount, Partitions = shiftedPartitions[nextLocation].Partitions };
            }

            return shiftedPartitions.OrderByDescending(p => p.Share).ToList();
        }
    }
}
