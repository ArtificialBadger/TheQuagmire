using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terra
{
    public class WorldReporter : IWorldReporter
    {
		public string Report(World world, ReportVerbosity verbosity)
		{
			var builder = new StringBuilder();

			builder.AppendLine(world.Name + " has a population of " + world.Population.ToString("N0"));

			foreach (var continent in world.Continents.OrderByDescending(c => c.Share))
			{
				builder.Append(FormatPartition(continent, world.Population, verbosity));
			}

			return builder.ToString();

		}

		private string FormatPartition(Partition partition, decimal total, ReportVerbosity verbosity, string prefix = "")
		{
			var builder = new StringBuilder();
			builder.Append(prefix);

			if (verbosity == ReportVerbosity.Full)
			{
				if (partition.Name == "Other")
                {
					builder.AppendLine((total * partition.Share).ToString("N0") + $" ({((total * partition.Share) / total).ToString("P")}), people live Elsewhere");
				}

				builder.AppendLine((total * partition.Share).ToString("N0") + $" ({((total * partition.Share) / total).ToString("P")}), people live in {partition.Name}");
			}
			else
            {
				builder.AppendLine($"{partition.Name}: {(total * partition.Share).ToString("N0")}");

			}

			if (partition.Partitions != null)
			{
				foreach (var innerPartition in partition.Partitions)
				{
					//builder.Append(prefix);
					builder.Append(FormatPartition(innerPartition, total * partition.Share, verbosity, prefix + "\t"));
				}
			}

			return builder.ToString();
		}
	}
}
