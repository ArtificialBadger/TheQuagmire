using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
	public struct Partition
	{
		public List<Partition> Partitions { get; set; }
		
		public decimal Share { get; set; }
		
		public string Name { get; set; }

		public Partition(Partition partition)
		{
			this.Partitions = partition.Partitions;
			this.Share = partition.Share;
			this.Name = partition.Name;
		}
	}
}
