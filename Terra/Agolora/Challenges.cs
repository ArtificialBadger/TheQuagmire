using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.Agolora
{
    public class Challenges
    {
		public static List<Challenge> AgoloraChallenges = new List<Challenge>()
		{
			new Challenge { Name = "Marked", Description = "Attain a Mark, 1 Ring", PassRate = .01m },
			new Challenge { Name = "2 Rings", Description = "Attain 3 Rings", PassRate = .1m },
			new Challenge { Name = "3 Rings", Description = "Attain 3 Rings", PassRate = .9999m },
			new Challenge { Name = "First Spire Challengers", Description = "Challenge the first Spire", PassRate = .1m, Lethal = true },
			new Challenge { Name = "First Spire Champion, 4 Rings", Description = "Pass the first Spire, 4 Rings", PassRate = .5m },
			new Challenge { Name = "6 Rings", Description = "Attain Six Rings", PassRate = .9m },
			new Challenge { Name = "Second Spire Challenger", Description = "Challenge the Second Spire", PassRate = .5m, Lethal = true },
			new Challenge { Name = "Second Spire Champion, 7 Rings", Description = "Pass the Second Spire, 7 Rings", PassRate = .25m },
			new Challenge { Name = "9 Rings", Description = "Attain nine Rings", PassRate = .1m },
			new Challenge { Name = "Third Spire Challenger", Description = "Challenge the Third Spire", PassRate = .1m, Lethal = true },
			new Challenge { Name = "Third Spire Champion, 10 Rings", Description = "Pass the Third Spire and attain the 10th Ring", PassRate = .1m },
			new Challenge { Name = "Fourth and Fifth Spire Champions, 11 Rings", Description = "Pass the Fourth and Fifth Spires and achieve the 11th Ring", PassRate = .1m },
		};
	}
}
