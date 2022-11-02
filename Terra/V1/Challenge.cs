using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.V1
{
    public struct Challenge
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal AchieveFromPreviousChallengeRate { get; set; }

        public bool Lethal { get; set; }
    }
}
