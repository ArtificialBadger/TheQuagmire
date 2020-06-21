using System;
using System.Collections.Generic;
using System.Text;

namespace Synger.Models
{
	public class Status
	{
		public DateTime? CreatedAt { get; set; }

		public string Message { get; set; }

		public bool IndicatesLimitedAvailability { get; set; }

		public DateTime? ExpiresAt { get; set; }

		public User User { get; set; }

		public string Id { get; set; }
	}
}
