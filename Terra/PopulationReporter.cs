using System;
using System.Collections.Generic;
using System.Text;

namespace Terra
{
    public class PopulationReporter : IPopulationReporter 
    {
		public string Report(decimal total, IList<Challenge> challenges)
		{
			decimal a = total;
			var builder = new StringBuilder();

			builder.AppendLine(total.ToString("N0") + " total population");
			builder.AppendLine((a - a * challenges[0].PassRate).ToString("N0") + $" Achieved Nothing, {((a - a * challenges[0].PassRate) / total).ToString("P1")}");

			for (int i = 0; i < challenges.Count; i++)
			{

				var challenge = challenges[i];
				var b = a;
				a *= challenge.PassRate;
				var stuckCount = a;
				var nextChallengePassCount = a * (i == challenges.Count - 1 ? 0 : challenges[i + 1].PassRate);
				if (a > 1)
				{
					if (i != challenges.Count - 1)
					{
						stuckCount -= nextChallengePassCount;
					}
					if (challenge.Lethal)
					{
						builder.AppendLine($"{stuckCount.ToString("N0")} died trying to {challenge.Description}, {(stuckCount / total).ToString("P4")}");
					}
					else
					{
						builder.AppendLine($"{stuckCount.ToString("N0")} were able to only {challenge.Description}, {(stuckCount / total).ToString("P4")}");
					}
				}
			}

			return builder.ToString();
		}
	}
}
