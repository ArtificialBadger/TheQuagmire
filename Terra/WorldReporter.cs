using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terra
{
    public class WorldReporter : IWorldReporter
    {
		public string Report(World world)
		{
			var builder = new StringBuilder();

			builder.AppendLine(world.Name + " has a population of " + world.Population.ToString("N0"));

			foreach (var continent in world.Continents.OrderByDescending(c => c.Share))
			{
				builder.Append(FormatPartition(continent, world.Population));
			}

			return builder.ToString();

		}

		private string FormatPartition(Partition partition, decimal total, string prefix = "")
		{
			var builder = new StringBuilder();
			builder.Append(prefix);

			builder.AppendLine((total * partition.Share).ToString("N0") + " people live in " + partition.Name + ", " + ((total * partition.Share) / total).ToString("P"));
			if (partition.Partitions != null)
			{
				foreach (var innerPartition in partition.Partitions)
				{
					builder.Append(prefix);
					builder.Append(FormatPartition(innerPartition, total * partition.Share, prefix + "\t"));
				}
			}

			return builder.ToString();
		}
	}
}
