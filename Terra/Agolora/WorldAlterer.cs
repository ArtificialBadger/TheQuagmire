using System;
using System.Collections.Generic;
using System.Text;

namespace Terra.Agolora
{
    public sealed class WorldAlterer : IWorldAlterer
    {
        public int Priority => int.MaxValue;

        public void Alter(World world)
        {
            var lesserEndingPercentage = .00005m;

            var omrIndex = world.Continents.FindIndex(c => c.Name == "Omr");
            if (omrIndex == -1)
            {
                return;
            }

            var omr = world.Continents[omrIndex];

            var lesserEndingIndex = world.Continents.FindIndex(c => c.Name == "Lesser Ending");
            if (lesserEndingIndex == -1)
            {
                return;
            }
            var lesserEnding = world.Continents[lesserEndingIndex];

            var diff = lesserEnding.Share - lesserEndingPercentage;
            lesserEnding.Share = lesserEndingPercentage;
            omr.Share += diff;

            world.Continents[lesserEndingIndex] = lesserEnding;
            world.Continents[omrIndex] = omr;
        }
    }
}
